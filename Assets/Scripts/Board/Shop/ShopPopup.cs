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
        SoundManager.Instance.PlaySound(SoundId.ShopBell);

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
        if (gameObject.activeInHierarchy)
        {
            SoundManager.Instance.PlaySound(SoundId.CloseDoor);
            gameObject.SetActive(false);
        }
    }

    private void ItemSelected(Item item)
    {
        SoundManager.Instance.PlaySound(SoundId.Buy);
        if (!item.IsFree)
        {
            GameManager.Instance.RegisterCoinsUpdate(-item.Price);
        }

        GameManager.Instance.RegisterInventoryUpdate(item.Id, ItemAction.Get);
        CloseClicked();
    }

    public void CloseClicked()
    {
        GameManager.Instance.RegisterToggleShop(false);
        GameManager.Instance.TakeStep();
    }
}