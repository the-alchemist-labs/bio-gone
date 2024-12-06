using System;

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
    public string[] PlayerIds { get; set; }
}