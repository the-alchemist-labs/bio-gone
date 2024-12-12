using System;
using System.Collections.Generic;
using UnityEngine;

public class Commander
{
    private readonly Dictionary<Command, Func<string, ICommand>> CommandMap = new()
    {
        { Command.RollDice, payload => new RollDiceCommand(payload) },
        { Command.MovePlayer, payload => new MovePlayerCommand(payload) },
        { Command.ModifyPlayerCoins, payload => new ModifyCoinsCommand(payload) },
        { Command.ToggleShop, payload => new ToggleShopCommand(payload) },
        { Command.NewTurn, payload => new NewTurnCommand(payload) },
        { Command.GainItem, payload => new GainItemCommand(payload) },
        { Command.ToggleBattle, payload => new ToggleBattleCommand(payload) },
        { Command.UpdateBattlePhase, payload => new UpdateBattlePhaseCommand(payload) },
    };
    
    public Commander()
    {
        SocketIO.Instance.RegisterEvent<CommandEvent>(SocketEvents.CommandReceived, ExecuteCommand);
    }

    public void PostCommand(CommandEvent command)
    {
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