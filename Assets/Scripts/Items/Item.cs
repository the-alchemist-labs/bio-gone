using System;
using UnityEngine;

public enum ItemId
{
    Helmet1,
    Helmet2,
    Heal1,
    Heal2,
}

public enum ItemType
{
    Equipment,
    Consumable,
}

public enum ItemEffectId
{
    BonusPower,
    HealUser,
}

[Serializable]
public class ItemEffect
{
    public ItemEffectId EffectId;
    public int Value;
}

public class Item : ScriptableObject
{
    public ItemId Id;
    public int Price;
    public bool IsFree;
    public ItemType ItemType;
    public string Description;
}

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Scriptable Objects/Items/Equipment")]
public class EquipItem : Item
{
    ItemType ItemType = ItemType.Equipment;
    public int BattlePowerBonus;
}

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Scriptable Objects/Items/Consumable")]
public class ConsumableItem : Item
{
    ItemType ItemType = ItemType.Consumable;
    public ItemEffect Effect;
}