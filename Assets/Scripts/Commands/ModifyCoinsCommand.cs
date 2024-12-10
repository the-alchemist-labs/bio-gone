
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ModifyCoinsCommandPayload
{ 
    public string PlayerId { get; set; }
    public int CoinsAmount { get; set; }
    public ModifyCoinsCommandPayload(string playerId, int coinsAmount)
    {
        PlayerId = playerId;
        CoinsAmount = coinsAmount;
    }
}

public class ModifyCoinsCommand : ICommand
{
    public static event Action<string, int> OnCoinsModified;

    private string PlayerId { get; set; }
    private int CoinsAmount { get; set; }
    
    public ModifyCoinsCommand(string payloadString)
    {
        ModifyCoinsCommandPayload payload = JsonConvert.DeserializeObject<ModifyCoinsCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        CoinsAmount = payload.CoinsAmount;
    }
    
    public void Execute()
    {
        OnCoinsModified?.Invoke(PlayerId, CoinsAmount);
    }
}