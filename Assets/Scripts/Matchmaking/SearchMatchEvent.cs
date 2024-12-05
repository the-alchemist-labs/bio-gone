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
    private string RoomId { get; set; }

    public MatchFoundEvent(string roomId)
    {
        this.RoomId = roomId;
    }
}