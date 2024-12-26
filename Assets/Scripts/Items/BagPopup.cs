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

public class BagPopup : MonoBehaviour
{
    [SerializeField] private Button useButton;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private Transform bagContent;
    [SerializeField] private GameObject bagItemPrefab;

    [CanBeNull] private ConsumableItem _selectedItem;
    private BagState _state;

    public void Display(BagState state)
    {
        _state = state;
        gameObject.SetActive(true);
        PopulateBagItems();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void UseItem()
    {
        if (_selectedItem == null) return;
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
}