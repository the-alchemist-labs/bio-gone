using System;
using UnityEngine;

public enum ItemId
{
    Helmet = 0,
    Shoes = 1,
    Googles = 2,
    Collar = 3,
    Backpack = 4,
    Gloves = 6,
    Weapon1 = 7,
    Weapon2 = 8,
    Knife = 9,
    Boost1 = 100,
    Boost2 = 101,
    Boost3 = 102,
    Heal1 = 103,
    Heal2 = 104,
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

public abstract class Item : ScriptableObject
{
    public ItemId Id;
    public string Name;
    public int Price;
    public bool IsFree;
    public ItemType ItemType;
    public string Description;
}