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
    public Phase Phase { get; private set; }
    public List<Player> Players { get; }
    public int PlayerTurn { get; private set; }

    public GameState(MatchFoundEvent matchFoundEvent)
    {
        RoomId = matchFoundEvent.RoomId;
        Phase = Phase.Start;
        Players = matchFoundEvent.PlayersData;
        PlayerTurn = matchFoundEvent.FirstTurnPlayer;
    }

    public void UpdatePhase(Phase phase)
    {
        Phase = phase;
    }

    public void UpdatePlayerTurn(int playerIndex)
    {
        PlayerTurn = playerIndex;
    }
}