public class Player
{ 
    public string PlayerId { get; private set; }
    public string Name { get; }
    public PlayerProfilePicture ProfilePicture { get; }
    public TileId Position { get; private set; }
    
    public int Coins { get; private set; }
    
    public Player(string playerId, string name,  PlayerProfilePicture profilePicture, TileId position)
    {
        PlayerId = playerId;    
        Name = name;
        ProfilePicture = profilePicture;
        Position = position;
        Coins = 0;
    }

    public void MovePlayer(TileId newPosition)
    {
        Position = newPosition;
    }
    
    public void UpdateCoins(int newValue)
    {
        Coins += newValue;
    } }