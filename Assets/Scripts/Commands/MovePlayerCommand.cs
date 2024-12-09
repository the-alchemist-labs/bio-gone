
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class MovePlayerCommandPayload
{ 
    public string PlayerId { get; set; }
    public TileId MoveTileId { get; set; }
    public MovePlayerCommandPayload(string playerId, TileId moveTileId)
    {
        PlayerId = playerId;
        MoveTileId = moveTileId;
    }
}

public class MovePlayerCommand : ICommand
{
    public static event Action<string, TileId> OnPlayerMove;

    private string PlayerId { get; set; }
    private TileId MoveTileId { get; set; }
    
    public MovePlayerCommand(string payloadString)
    {
        MovePlayerCommandPayload payload = JsonConvert.DeserializeObject<MovePlayerCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        MoveTileId = payload.MoveTileId;
    }
    
    public void Execute()
    {
        OnPlayerMove?.Invoke(PlayerId, MoveTileId);
    }
}