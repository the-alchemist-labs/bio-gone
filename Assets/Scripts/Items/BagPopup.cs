using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BagState
{
    Idle,
    Battle,
    OpponentBattle,
}

public class BagPopup : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemEffect;
    [SerializeField] private TMP_Text itemPrice;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Button useButton;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private Transform bagContent;
    [SerializeField] private GameObject bagItemPrefab;

    private ConsumableItem _selectedItem;
    private BagState _state;

    public void Display(BagState state)
    {
        _state = state;
        gameObject.SetActive(true);
        useButton.interactable = state != BagState.Idle;
        UpdateItemDetails(GameManager.Instance.GameState.GetPlayer().GetBagItems().First().Id);
        PopulateBagItems();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void UseItem()
    {
        switch (_state)
        {
            // Check if it's targeting player or monster
            case BagState.Battle:
                GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Result,
                    new BattleItemUsed(PlayerProfile.Instance.Id, _selectedItem.Id, BattleTarget.Monster));
                break;
            case BagState.OpponentBattle:
                GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.PlayerAction,
                    new BattleItemUsed(PlayerProfile.Instance.Id, _selectedItem.Id, BattleTarget.Player));
                break;
        }

        Close();
    }

    private void PopulateBagItems()
    {
        bagContent.Cast<Transform>().ToList().ForEach(child =>
        {
            child.GetComponent<BagItem>().OnItemSelected -= UpdateItemDetails;
            Destroy(child.gameObject);
        });

        List<ConsumableItem> bagItems = GameManager.Instance.GameState.GetPlayer().GetBagItems();

        foreach (Item item in bagItems)
        {
            GameObject newBagItem = Instantiate(bagItemPrefab, bagContent);
            if (newBagItem.TryGetComponent(out BagItem bagItem))
            {
                bagItem.Initialize(item.Id);
                bagItem.OnItemSelected += UpdateItemDetails;
            }
        }
    }

    private void UpdateItemDetails(ItemId itemId)
    {
        _selectedItem = ItemCatalog.Instance.GetItem<ConsumableItem>(itemId);
        itemName.text = _selectedItem.Name;
        itemEffect.text = $"{_selectedItem.Effect.EffectId} {_selectedItem.Effect.Value}";
        itemPrice.text = _selectedItem.Price.ToString();
        itemDescription.text = _selectedItem.Description;
    }
}