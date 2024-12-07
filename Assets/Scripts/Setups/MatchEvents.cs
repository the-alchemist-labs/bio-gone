using System;
using System.Collections.Generic;

[Serializable]
public class SearchMatchEvent
{
    public string PlayerId { get; set; }

    public SearchMatchEvent(string playerId)
    {
        this.PlayerId = playerId;
    }
}

[Serializable]
public class MatchFoundEvent
{
    public string RoomId { get; set; }
    public List<Player> PlayersData{ get; set; }
    public int FirstTurnPlayer { get; set; }
}

public class MatchFoundResults
{
    public static MatchFoundEvent Instance;
}