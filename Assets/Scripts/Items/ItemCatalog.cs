using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCatalog : MonoBehaviour
{
    private const string SCRIPTABLE_OBJECT_CONSUMABLES_ITEMS_PATH = "ScriptableObjects/Items/Consumables";
    private const string SCRIPTABLE_OBJECT_EQUIPMENTS_ITEMS_PATH = "ScriptableObjects/Items/Equipments";

    public static ItemCatalog Instance { get; private set; }

    public List<Item> Items { get; private set; }

    public List<Item> ConsumableItems { get; private set; }
    public List<Item> EquipmentItems { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        ConsumableItems = Resources.LoadAll<Item>(SCRIPTABLE_OBJECT_CONSUMABLES_ITEMS_PATH)
            .Select(Instantiate)
            .ToList();

        EquipmentItems = Resources.LoadAll<Item>(SCRIPTABLE_OBJECT_EQUIPMENTS_ITEMS_PATH)
            .Select(Instantiate)
            .ToList();

        Items = ConsumableItems.Concat(EquipmentItems).ToList();
    }

    public T GetItem<T>(ItemId id) where T : Item
    {
        return Items.Find(el => el.Id == id) as T;
    }
}