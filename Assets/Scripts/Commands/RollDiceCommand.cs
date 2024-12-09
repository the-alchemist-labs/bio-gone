
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
    public static event Action<string, int> OnDiceRolled;

    private string PlayerId { get; }
    private int DiceValue { get; }
    
    public RollDiceCommand(string payloadString)
    {
        Debug.Log(payloadString);
        RollDiceCommandPayload payload = JsonConvert.DeserializeObject<RollDiceCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        DiceValue = payload.DiceValue;
    }
    
    public void Execute()
    {
        Debug.Log($"[RollDice] {PlayerId} : {DiceValue}");
        OnDiceRolled?.Invoke(PlayerId, DiceValue);
    }
}