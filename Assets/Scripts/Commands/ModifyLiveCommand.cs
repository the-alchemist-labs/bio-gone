
using System;
using Newtonsoft.Json;

[Serializable]
public class ModifyLiveCommandPayload
{ 
    public string PlayerId { get; set; }
    public int LivesModifier { get; set; }
    public ModifyLiveCommandPayload(string playerId, int livesModifier)
    {
        PlayerId = playerId;
        LivesModifier = livesModifier;
    }
}

public class ModifyLiveCommand : ICommand
{
    public static event Action<string, int> OnLivesModified;

    private string PlayerId { get; }
    private int LivesModifier { get; }
    
    public ModifyLiveCommand(string payloadString)
    {
        ModifyLiveCommandPayload payload = JsonConvert.DeserializeObject<ModifyLiveCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
        LivesModifier = payload.LivesModifier;
    }
    
    public void Execute()
    {
        OnLivesModified?.Invoke(PlayerId, LivesModifier);
    }
}