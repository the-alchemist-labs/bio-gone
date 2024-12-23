using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShopService
{
    public List<Item> GetItems()
    {
        List<Item> consumableItem = ItemCatalog.Instance.ConsumableItems;
        List<Item> equipItems = ItemCatalog.Instance.EquipmentItems;
        List<Item> freeItems = ItemCatalog.Instance.Items.Where(i => i.IsFree).ToList();

        
        List<Item> shopItems = new List<Item>();

        foreach (List<Item> list in new[] { freeItems, consumableItem, equipItems })
        {
            shopItems.Add(GetRandomItem(list, shopItems));
        }

        return shopItems;
    }

    private Item GetRandomItem(List<Item> fromList, List<Item> notFromList)
    {
        var filteredList = fromList.Except(notFromList).ToList();
        return filteredList[Random.Range(0, filteredList.Count)];
    }
}