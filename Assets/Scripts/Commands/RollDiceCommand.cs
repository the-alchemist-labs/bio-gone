
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class RollDiceCommandPayload
{ 
    public string PlayerId { get; set;  }
    public int DiceValue { get; set; }
    
    public RollDiceCommandPayload(string playerId, int diceValue)
    {
        PlayerId = playerId;
        DiceValue = diceValue;
    }
}

public class RollDiceCommand : ICommand
{
    private string PlayerId { get; set; }
    private int DiceValue { get; set; }
    
    public RollDiceCommand(string payloadString)
    {
        Debug.Log(payloadString);
        RollDiceCommandPayload payload = JsonConvert.DeserializeObject<RollDiceCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        DiceValue = payload.DiceValue;
    }
    
    public void Execute()
    {
        Debug.Log($"RollDice: {PlayerId} - {DiceValue}");
    }
}