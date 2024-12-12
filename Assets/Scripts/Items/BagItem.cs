using System;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    public event Action<ItemId> OnItemSelected;
    [SerializeField] private Image itemImage;

    ItemId _itemId;
    
    public void Initialize(ItemId itemId)
    {
        _itemId = itemId;
        itemImage.sprite = Resources.Load<Sprite>($"Sprites/Items/Consumables/{itemId}");
    }

    public void OnClick()
    {
        OnItemSelected?.Invoke(_itemId);
    }
}
