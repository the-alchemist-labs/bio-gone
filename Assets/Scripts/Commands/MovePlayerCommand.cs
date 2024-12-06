
using System;
using UnityEngine;

[Serializable]
public class MovePlayerCommandPayload
{ 
    public string PlayerId { get; set; }
    public TileId MoveTileId { get; set; }
}

public class MovePlayerCommand : ICommand
{
    private string PlayerId { get; set; }
    private TileId MoveTileId { get; set; }
    
    public MovePlayerCommand(string payloadString)
    {
        MovePlayerCommandPayload payload = JsonUtility.FromJson<MovePlayerCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        MoveTileId = payload.MoveTileId;
    }
    
    public void Execute()
    {
        Player player = GameManager.Instance.GameState.Players.Find(p => p.PlayerId == PlayerId);
        player.MovePlayer(MoveTileId);
    }
}