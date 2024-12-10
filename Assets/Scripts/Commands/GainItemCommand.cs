using System;
using Newtonsoft.Json;

[Serializable]
public class GainItemCommandPayload
{
    public string PlayerId { get; set; }
    public ItemId ItemId { get; set; }

    public GainItemCommandPayload(string playerId, ItemId itemId)
    {
        PlayerId = playerId;
        ItemId = itemId;
    }
}

public class GainItemCommand : ICommand
{
    public static event Action<string, ItemId> OnItemGained;

    private string PlayerId { get; }
    private ItemId ItemId { get; }

    public GainItemCommand(string payloadString)
    {
        GainItemCommandPayload payload = JsonConvert.DeserializeObject<GainItemCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        ItemId = payload.ItemId;
    }

    public void Execute()
    {
        OnItemGained?.Invoke(PlayerId, ItemId);
    }
}