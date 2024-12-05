using System;
using System.Collections.Generic;

public class CommandEvent
{
    public string RoomId { get; set; }
    public Command CommandType { get; set; }
    public ICommandPayload Payload { get; set; }

    public CommandEvent(string roomId, Command commandType, ICommandPayload payload)
    {
        RoomId = roomId;
        CommandType = commandType;
        Payload = payload;
    }
}

public class Commander
{
    private readonly Dictionary<Command, ICommandFactory> CommandFactories = new()
    {
        { Command.RollDice, new RollDiceCommandFactory() },
        { Command.MovePlayer, new MovePlayerCommandFactory() },
    };
    
    public Commander()
    {
        SocketIO.Instance.RegisterEvent<CommandEvent>(SocketEvents.CommandReceived, ExecuteCommand);
    }

    public void PostCommand(CommandEvent command)
    {
        SocketIO.Instance.EmitEvent(SocketEvents.PostCommand, command);
    }
    
    private void ExecuteCommand(CommandEvent @event)
    {
        if (CommandFactories.TryGetValue(@event.CommandType, out var factory))
        {
            ICommand command = factory.CreateCommand(@event.Payload);
            command.Execute();
        }
        else
        {
            throw new Exception($"Unknown command type: {@event.CommandType}");
        }
    }
}