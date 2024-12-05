using System.Collections.Generic;

public enum Phase
{
    Start,
    RollDice,
    Move,
    EnterShop,
    Battle,
    End
}

public class GameState
{
    public string RoomId { get; }
    public Phase Phase { get; }
    public List<Player> Players { get; }

    public GameState(string roomId, List<Player> players)
    {
        RoomId = roomId;
        Phase = Phase.Start;
        Players = players;
    }
}