
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class GainCoinsCommandPayload
{ 
    public string PlayerId { get; set; }
    public int CoinsAmount { get; set; }
    public GainCoinsCommandPayload(string playerId, int coinsAmount)
    {
        PlayerId = playerId;
        CoinsAmount = coinsAmount;
    }
}

public class GainCoinsCommand : ICommand
{
    public static event Action<string, int> OnCoinsGained;

    private string PlayerId { get; set; }
    private int CoinsAmount { get; set; }
    
    public GainCoinsCommand(string payloadString)
    {
        GainCoinsCommandPayload payload = JsonConvert.DeserializeObject<GainCoinsCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        CoinsAmount = payload.CoinsAmount;
    }
    
    public void Execute()
    {
        OnCoinsGained?.Invoke(PlayerId, CoinsAmount);
    }
}