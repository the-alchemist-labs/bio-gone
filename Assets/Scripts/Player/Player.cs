using System.Collections.Generic;
using System.Linq;

public enum PlayerProfilePicture
{
    Bob,
    Dana,
}

public class Player
{
    public string Id { get; private set; }
    public string Name { get; }
    public PlayerProfilePicture ProfilePicture { get; }
    public TileId Position { get; private set; }
    public int Lives { get; private set; }
    public int Coins { get; private set; }
    public int Level { get; }
    public int BattlePower => CalculatePower();
    private List<Item> Items { get; }
    
    public Player(string playerId, string name, PlayerProfilePicture profilePicture, TileId position)
    {
        Id = playerId;
        Name = name;
        ProfilePicture = profilePicture;
        Position = position;
        Coins = 0;
        Level = 1;
        Lives = Consts.DefaultLives;
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

    public void ModifyLives(int modifier)
    {
        if (Lives + modifier > Consts.DefaultLives) Lives = Consts.DefaultLives;
        if (Lives + modifier <= 0)
        {
            Lives = 0;
            // end player
            return;
        }
        
        Lives += modifier;
    }
    public void AddItem(ItemId itemId)
    {
        Item newItem = ItemCatalog.Instance.GetItem<Item>(itemId);
        Items.Add(newItem);
    }

    public List<ConsumableItem> GetBagItems()
    {
        return Items.Where(i => i.ItemType == ItemType.Consumable).Cast<ConsumableItem>().ToList();
    }
    
    public List<EquipItem> GetEquippedItems()
    {
        return Items.Where(i => i.ItemType == ItemType.Equipment).Cast<EquipItem>().ToList();
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