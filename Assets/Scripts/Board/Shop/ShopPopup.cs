using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : MonoBehaviour, IPopup
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Image shoppingImage;

    public void Display()
    {
        gameObject.SetActive(true);
        bool isYourTurn = GameManager.Instance.GameState.IsYourTurn();
        
        closeButton.gameObject.SetActive(isYourTurn);
        content.gameObject.SetActive(isYourTurn);
        shoppingImage.gameObject.SetActive(!isYourTurn);
        if (isYourTurn) RestockShop();
    }

    private void RestockShop()
    {
        List<Item> shopItems = BoardManager.Instance.ShopService.GetItems();
        
        content.Cast<Transform>().ToList().ForEach(child =>
        {
            child.GetComponent<ShopItem>().OnItemSelected -= ItemSelected;
            Destroy(child.gameObject);
        });
        
        foreach (Item item in shopItems)
        {
            GameObject newShopItem = Instantiate(shopItemPrefab, content);
            if (newShopItem.TryGetComponent(out ShopItem shopItem))
            {
                shopItem.OnItemSelected += ItemSelected;
                shopItem.InitializeShopItem(item);
            }
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
    
    private void ItemSelected(ItemId itemId)
    {
        GameManager.Instance.RegisterInventoryUpdate(itemId, ItemAction.Get);
        CloseClicked();
    }
    
    public void CloseClicked()
    {
        GameManager.Instance.RegisterToggleShop(false);
        GameManager.Instance.TakeStep();
    }
}
