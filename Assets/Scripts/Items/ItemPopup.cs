using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image effectImage;
    [SerializeField] private TMP_Text effectText;
    [SerializeField] private TMP_Text priceText;

    private Dictionary<ItemEffectId, Sprite> _effectSprites;

    void Awake()
    {
        _effectSprites = new Dictionary<ItemEffectId, Sprite>
        {
            { ItemEffectId.BonusPower, Resources.Load<Sprite>("Sprites/Game/BP") },
            { ItemEffectId.HealUser, Resources.Load<Sprite>("Sprites/Game/Heart") }
        };
    }

    public void OpenPopup(Item item)
    {
        gameObject.SetActive(true);

        itemImage.sprite = ItemCatalog.Instance.GetItemSprite(item.Id);
        nameText.text = item.Name;
        descriptionText.text = item.Description;
        priceText.text = item.Price.ToString();

        if (item is ConsumableItem consumable)
        {
            effectImage.sprite = _effectSprites[consumable.Effect.EffectId];
            effectText.text = consumable.Effect.Value.ToString();
        }
        else if (item is EquipItem equip)
        {
            effectImage.sprite = _effectSprites[ItemEffectId.BonusPower];
            effectText.text = equip.BattlePowerBonus.ToString();
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}