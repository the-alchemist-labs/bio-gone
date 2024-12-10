using System.Collections.Generic;
using System.Linq;

public class Player
{
    public string Id { get; private set; }
    public string Name { get; }
    public PlayerProfilePicture ProfilePicture { get; }
    public TileId Position { get; private set; }

    public int Coins { get; private set; }

    private List<Item> Items { get; set; }
    public int BattlePower => CalculatePower();
    public int Level { get; set; }

    public Player(string playerId, string name, PlayerProfilePicture profilePicture, TileId position)
    {
        Id = playerId;
        Name = name;
        ProfilePicture = profilePicture;
        Position = position;
        Coins = 0;
        Level = 1;
        Items = new List<Item>();
    }

    public void MovePlayer(TileId newPosition)
    {
        Position = newPosition;
    }

    public void UpdateCoins(int newValue)
    {
        Coins += newValue;
    }

    public void AddItem(ItemId itemId)
    {
        Item newItem = ItemCatalog.Instance.GetItem(itemId);
        Items.Add(newItem);
    }

    public List<Item> GetBagItems()
    {
        return Items.Where(i => i.ItemType == ItemType.Consumable).ToList();
    }
    
    public List<Item> GetEquippedItems()
    {
        return Items.Where(i => i.ItemType == ItemType.Equipment).ToList();
    }

    private int CalculatePower()
    {
        int levelsPower = Level * Consts.LevelPowerModifier;
        int itemsPower = Items
            .Where(i => i.ItemType == ItemType.Equipment)
            .OfType<EquipItem>()
            .Sum(e => e.BattlePowerBonus);
        return levelsPower + itemsPower;
    }
}