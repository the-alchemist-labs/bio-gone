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
    Phase Phase { get; set; }

    public GameState()
    {
        Phase = Phase.Start;
    }
}