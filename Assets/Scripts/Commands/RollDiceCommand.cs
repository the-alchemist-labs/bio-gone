
using System;
using UnityEngine;

public class RollDiceCommandPayload : ICommandPayload
{ 
    public string PlayerId { get; }
    public TileId MoveTileId { get; }
}

public class RollDiceCommandFactory : ICommandFactory
{
    public ICommand CreateCommand(ICommandPayload payload)
    {
        if (payload is RollDiceCommandPayload rollDicePayload)
        {
            return new RollDiceCommand(rollDicePayload);
        }
        throw new InvalidCastException("Invalid payload type for RollDiceCommand");
    }
}
public class RollDiceCommand : ICommand
{
    private string _playerId;
    private TileId _moveTileId;
    
    public RollDiceCommand(RollDiceCommandPayload payload)
    {
        _playerId = payload.PlayerId;
        _moveTileId = payload.MoveTileId;
    }
    
    public void Execute()
    {
        Debug.Log($"{_playerId} - {_moveTileId}");
    }
}