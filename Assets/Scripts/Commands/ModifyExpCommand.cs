
using System;
using Newtonsoft.Json;

[Serializable]
public class ModifyExpCommanddPayload
{ 
    public string PlayerId { get; set; }
    public int Amount { get; set; }
    public ModifyExpCommanddPayload(string playerId, int amount)
    {
        PlayerId = playerId;
        Amount = amount;
    }
}

public class ModifyExpCommand : ICommand
{
    public static event Action<string, int> OnExpUpdated;

    private string PlayerId { get; }
    private int Amount { get; }
    
    public ModifyExpCommand(string payloadString)
    {
        ModifyExpCommanddPayload payload = JsonConvert.DeserializeObject<ModifyExpCommanddPayload>(payloadString);
        PlayerId = payload.PlayerId;
        Amount = payload.Amount;
    }
    
    public void Execute()
    {
        OnExpUpdated?.Invoke(PlayerId, Amount);
    }
}