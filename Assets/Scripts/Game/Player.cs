public class Player
{ 
    public string PlayerId { get; }
    TileId PlayerPosition { get; set; }

    public Player(string playerId, TileId playerPosition)
    {
        PlayerId = playerId;
        PlayerPosition = playerPosition;
    }

    public void MovePlayer(TileId newPosition)
    {
        PlayerPosition = newPosition;
        GameEvents.PlayerMove(PlayerId, newPosition);
    }
}