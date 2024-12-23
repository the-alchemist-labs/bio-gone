using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<ItemId> OnItemSelected;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private HorizontalLayoutGroup priceLayout;
    [SerializeField] private Button priceButton;
    [SerializeField] private Image coinImage;
    [SerializeField] private TMP_Text priceText;

    private Item _item;
    private Player _player;

    public void InitializeShopItem(Item item)
    {
        _item = item;
        _player = GameManager.Instance.GameState.GetPlayer(PlayerProfile.Instance.Id);

        nameText.text = item.Name;
        itemImage.sprite =  ItemCatalog.Instance.GetItemSprite(item.Id);
        priceText.text = item.IsFree ? "Free" : item.Price.ToString();
        coinImage.gameObject.SetActive(!item.IsFree);
        LayoutRebuilder.ForceRebuildLayoutImmediate(priceLayout.GetComponent<RectTransform>());

        priceButton.interactable = item.IsFree || item.Price <= _player.Coins;
    }

    public void SelectItem()
    {
        if (!_item.IsFree)
        {
            GameManager.Instance.RegisterCoinsUpdate(-_item.Price);
        }

        OnItemSelected?.Invoke(_item.Id);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter == itemImage.gameObject)
        {
            PopupManager.Instance.itemPopup.OpenPopup(_item);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == itemImage.gameObject)
        {
            PopupManager.Instance.itemPopup.ClosePopup();
        }
    }

    private void HandleTouchStart()
    {
        // Perform your logic here for touch start
        Debug.Log("Item Image Touch Start Logic");
    }
}
