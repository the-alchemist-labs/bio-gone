using System;
using System.Collections.Generic;

public static class Commander
{
    private static readonly Dictionary<Command, ICommandFactory> CommandFactories = new()
    {
        { Command.RollDice, new RollDiceCommandFactory() },
    };

    public static void QueueCommand(Command commandType, ICommandPayload payload)
    {
        if (CommandFactories.TryGetValue(commandType, out var factory))
        {
            ICommand command = factory.CreateCommand(payload);
            GameManager.Instance.CommandsQueue.AddCommand(command);
        }
        else
        {
            throw new Exception($"Unknown command type: {commandType}");
        }
    }
}