public class SearchMatchEvent
{
    private string PlayerId { get; set; }

    public SearchMatchEvent(string playerId)
    {
        this.PlayerId = playerId;
    }
}

public class MatchFoundEvent
{
    public string RoomId { get; private set; }
    public string[] PlayerIds { get; private set; }
}