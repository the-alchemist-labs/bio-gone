using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BagState
{
    Idle,
    Battle,
    OpponentBattle,
}

public class BagPopup : MonoBehaviour, IPopup
{
    [SerializeField] private Button useButton;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private Transform bagContent;
    [SerializeField] private GameObject bagItemPrefab;
    [SerializeField] private GameObject targetContainer;
    [SerializeField] private Image monsterImage;
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text useButtonText;

    [CanBeNull] private ConsumableItem _selectedItem;
    private BagState _state;
    private int? _interruptTimer;
    
    public void Display(BagState state)
    {
        Battle battle = GameManager.Instance.Battle;
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{battle.Player.ProfilePicture}");
        monsterImage.sprite = Resources.Load<Sprite>($"Sprites/Monsters/{battle.Monster.Id}");
        
        _state = state;
        gameObject.SetActive(true);
        PopulateBagItems();
        if (!battle.IsInBattle()) StartCoroutine(nameof(SetTimer));
        else useButtonText.text = "Use";
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    public void UseItem()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        if (_selectedItem == null) return;
        targetContainer.SetActive(true);
    }

    public void OnTargetClicked(int targetIndex)
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        BattleTarget[] targets = { BattleTarget.Player, BattleTarget.Monster };
        
        switch (_state)
        {
            case BagState.Battle:
                GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Result,
                    new BattleItemUsed(PlayerProfile.Instance.Id, _selectedItem.Id, targets[targetIndex]));
                break;
            case BagState.OpponentBattle:
                GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.PlayerAction,
                    new BattleItemUsed(PlayerProfile.Instance.Id, _selectedItem.Id, targets[targetIndex]));
                break;
        }

        targetContainer.SetActive(false);
        ClosePopup();
    }
    
    private void PopulateBagItems()
    {
        bagContent.Cast<Transform>().ToList().ForEach(child =>
        {
            child.GetComponent<BagItem>().OnItemSelected -= UpdateItemDetails;
            Destroy(child.gameObject);
        });

        List<ConsumableItem> bagItems = GameManager.Instance.GameState.GetPlayer().GetBagItems();

        foreach (ConsumableItem item in bagItems)
        {
            GameObject newBagItem = Instantiate(bagItemPrefab, bagContent);
            if (newBagItem.TryGetComponent(out BagItem bagItem))
            {
                bagItem.Initialize(item, IsSelectedItem(item.Id));
                bagItem.OnItemSelected += UpdateItemDetails;
            }
        }
    }

    private void UpdateItemDetails(ItemId itemId)
    {
        _selectedItem = ItemCatalog.Instance.GetItem<ConsumableItem>(itemId);
        PopulateBagItems();
        useButton.interactable = true;
    }

    private bool IsSelectedItem(ItemId itemId)
    {
        return _selectedItem != null && _selectedItem.Id == itemId;
    }
    
    private IEnumerator SetTimer()
    {
        _interruptTimer = Consts.InterruptSeconds;
        
        while (_interruptTimer > 0)
        {
            useButtonText.text = $"Use ({_interruptTimer--})";
            yield return new WaitForSeconds(1f);
        }
        
        ClosePopup();
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.PlayerAction);
    }
}