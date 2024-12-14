
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ModifyCoinsCommandPayload
{ 
    public string PlayerId { get; set; }
    public int CoinsModifier { get; set; }
    public ModifyCoinsCommandPayload(string playerId, int coinsModifier)
    {
        PlayerId = playerId;
        CoinsModifier = coinsModifier;
    }
}

public class ModifyCoinsCommand : ICommand
{
    public static event Action<string, int> OnCoinsModified;

    private string PlayerId { get; }
    private int CoinsModifier { get; }
    
    public ModifyCoinsCommand(string payloadString)
    {
        ModifyCoinsCommandPayload payload = JsonConvert.DeserializeObject<ModifyCoinsCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        CoinsModifier = payload.CoinsModifier;
    }
    
    public void Execute()
    {
        OnCoinsModified?.Invoke(PlayerId, CoinsModifier);
    }
}