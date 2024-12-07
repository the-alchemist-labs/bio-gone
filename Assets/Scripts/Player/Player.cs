
public class Player
{ 
    public string PlayerId { get; private set; }
    string Name { get; set; }
    TileId PlayerPosition { get; set; }


    public Player(string playerId, string name)
    {
        PlayerId = playerId;    
        Name = name;
        PlayerPosition = TileId.None;
    }

    public void MovePlayer(TileId newPosition)
    {
        PlayerPosition = newPosition;
        GameEvents.PlayerMove(PlayerId, newPosition);
    }
}