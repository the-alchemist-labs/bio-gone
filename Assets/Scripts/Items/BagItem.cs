using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    public event Action<ItemId> OnItemSelected;
    [SerializeField] private Image borderImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image effectImage;
    [SerializeField] private TMP_Text effectText;

    ConsumableItem _item;
    
    public void Initialize(ConsumableItem item, bool isSelected)
    {
        _item = item;
        itemImage.sprite = ItemCatalog.Instance.GetItemSprite(item.Id);
        effectImage.sprite = ItemCatalog.Instance.GetItemEffectSprite(item.Effect.EffectId);
        effectText.text = item.Effect.Value.ToString();
        borderImage.color = isSelected ? Color.red : Color.white;
    }

    public void OnClick()
    {
        OnItemSelected?.Invoke(_item.Id);
    }
}
