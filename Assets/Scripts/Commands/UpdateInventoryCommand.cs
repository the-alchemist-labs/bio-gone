using System;
using Newtonsoft.Json;

public enum ItemAction
{
    Get,
    Remove,
}

[Serializable]
public class UpdateInventoryCommandPayload
{
    public string PlayerId { get; set; }
    public ItemId ItemId { get; set; }
    public ItemAction Action { get; set; }

    public UpdateInventoryCommandPayload(string playerId, ItemId itemId, ItemAction action)
    {
        PlayerId = playerId;
        ItemId = itemId;
        Action = action;
    }
}

public class UpdateInventoryCommand : ICommand
{
    public static event Action<string, ItemId, ItemAction> OnItemGained;

    private string PlayerId { get; }
    private ItemId ItemId { get; }
    private ItemAction Action { get; }

    public UpdateInventoryCommand(string payloadString)
    {
        UpdateInventoryCommandPayload payload = JsonConvert.DeserializeObject<UpdateInventoryCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        ItemId = payload.ItemId;
        Action = payload.Action;
    }

    public void Execute()
    {
        OnItemGained?.Invoke(PlayerId, ItemId, Action);
    }
}