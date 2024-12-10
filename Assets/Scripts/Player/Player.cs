using System.Collections.Generic;

public class Player
{
    public string Id { get; private set; }
    public string Name { get; }
    public PlayerProfilePicture ProfilePicture { get; }
    public TileId Position { get; private set; }

    public int Coins { get; private set; }

    public List<ItemId> Items { get; private set; }

    public Player(string playerId, string name, PlayerProfilePicture profilePicture, TileId position)
    {
        Id = playerId;
        Name = name;
        ProfilePicture = profilePicture;
        Position = position;
        Coins = 0;
        Items =  new List<ItemId>();
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
        Items.Add(itemId);
    }
}