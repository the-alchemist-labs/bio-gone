
using System;
using UnityEngine;

public class RollDiceCommandPayload : ICommandPayload
{ 
    public string PlayerId { get; set;  }
    public int DiceValue { get; set; }

    public RollDiceCommandPayload(string playerId, int diceValue)
    {
        PlayerId = playerId;
        DiceValue = diceValue;
    }
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
    private string PlayerId { get; set; }
    private int DiceValue { get; set; }
    
    public RollDiceCommand(RollDiceCommandPayload payload)
    {
        PlayerId = payload.PlayerId;
        DiceValue = payload.DiceValue;
    }
    
    public void Execute()
    {
        Debug.Log($"{PlayerId} - {DiceValue}");
    }
}