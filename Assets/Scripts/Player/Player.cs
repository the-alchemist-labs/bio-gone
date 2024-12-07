
using System;

public class Player
{ 
    public string PlayerId { get; private set; }
    string Name { get; set; }
    PlayerProfilePicture ProfilePicture { get; set; }
    public TileId Position { get; private set; }

    public static event Action<string, TileId> OnPlayerMove;

    public Player(string playerId, string name,  PlayerProfilePicture profilePicture, TileId position)
    {
        PlayerId = playerId;    
        Name = name;
        ProfilePicture = profilePicture;
        Position = position;
    }

    public void MovePlayer(TileId newPosition)
    {
        Position = newPosition;
        OnPlayerMove?.Invoke(PlayerId, newPosition);
    }
}