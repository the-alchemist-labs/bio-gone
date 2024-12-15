
using System;
using Newtonsoft.Json;

[Serializable]
public class LevelUpCommandPayload
{ 
    public string PlayerId { get; set; }
    public LevelUpCommandPayload(string playerId)
    {
        PlayerId = playerId;
    }
}

public class LevelUpCommand : ICommand
{
    public static event Action<string> OnLevelUp;

    private string PlayerId { get; }
    
    public LevelUpCommand(string payloadString)
    {
        LevelUpCommandPayload payload = JsonConvert.DeserializeObject<LevelUpCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
    }
    
    public void Execute()
    {
        OnLevelUp?.Invoke(PlayerId);
    }
}