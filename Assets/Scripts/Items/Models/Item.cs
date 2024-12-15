using System;
using UnityEngine;

public enum ItemId
{
    Helmet = 0,
    Boots = 1,
    Heal1 = 2,
    Heal2 = 3,
    Boost1 = 4,
    Boost2 = 5,
    Knife = 6,
    Sword = 7,
    Axe = 8,
    Armor = 9,
    Hack = 10
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