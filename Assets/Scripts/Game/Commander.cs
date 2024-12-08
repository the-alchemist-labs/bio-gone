using System;
using System.Collections.Generic;
using UnityEngine;

public class Commander
{
    private readonly Dictionary<Command, Func<string, ICommand>> CommandMap = new()
    {
        { Command.RollDice, payload => new RollDiceCommand(payload) },
        { Command.MovePlayer, payload => new MovePlayerCommand(payload) },
    };
    
    public Commander()
    {
        SocketIO.Instance.RegisterEvent<CommandEvent>(SocketEvents.CommandReceived, ExecuteCommand);
    }

    public void PostCommand(CommandEvent command)
    {
        // string json = JsonConvert.SerializeObject(commandEvent);

        SocketIO.Instance.EmitEvent(SocketEvents.PostCommand, command);
    }
    
    private void ExecuteCommand(CommandEvent message)
    {
        Debug.Log(message);
        if (CommandMap.TryGetValue(message.CommandType, out var command))
        {
            var commandInstance = command(message.Payload);
            commandInstance.Execute();
        }
        else
        {
            throw new Exception($"Unknown command type: {message.CommandType}");
        }
    }
}