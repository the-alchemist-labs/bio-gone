
using System;

public class MovePlayerCommandPayload : ICommandPayload
{ 
    public string PlayerId { get; set; }
    public TileId MoveTileId { get; set; }
}

public class MovePlayerCommandFactory : ICommandFactory
{
    public ICommand CreateCommand(ICommandPayload payload)
    {
        if (payload is MovePlayerCommandPayload movePlayerPayload)
        {
            return new MovePlayerCommand(movePlayerPayload);
        }
        throw new InvalidCastException("Invalid payload type for MovePlayerCommand");
    }
}
public class MovePlayerCommand : ICommand
{
    private string PlayerId { get; set; }
    private TileId MoveTileId { get; set; }
    
    public MovePlayerCommand(MovePlayerCommandPayload payload)
    {
        PlayerId = payload.PlayerId;
        MoveTileId = payload.MoveTileId;
    }
    
    public void Execute()
    {
        Player player = GameManager.Instance.GameState.Players.Find(p => p.PlayerId == PlayerId);
        player.MovePlayer(MoveTileId);
    }
}