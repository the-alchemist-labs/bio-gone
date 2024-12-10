using UnityEngine;
using UnityEngine.UI;

public class EquippedItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public void InitializeEquippedItem(Item item)
    {
        itemImage.sprite = Resources.Load<Sprite>($"Sprites/Items/Equipments/{item.Id}");
    }
}