
using System;
using Newtonsoft.Json;

[Serializable]
public class ToggleBattleCommandPayload
{ 
    public bool IsOpen { get; set; }
    public string PlayerId { get; set; }
    public MonsterId? MonsterId { get; set; }
    
    public ToggleBattleCommandPayload(bool isOpen, string playerId,  MonsterId? monsterId = null)
    {
        IsOpen = isOpen;
        PlayerId = playerId;
        MonsterId = monsterId;
    }
}

public class ToggleBattleCommand : ICommand
{
    public static event Action<bool, string, MonsterId?> OnBattleToggled;

    public bool IsOpen { get; }
    public string PlayerId { get; set; }
    public MonsterId? MonsterId { get; set; }

    public ToggleBattleCommand(string payloadString)
    {
        ToggleBattleCommandPayload payload = JsonConvert.DeserializeObject<ToggleBattleCommandPayload>(payloadString);
        IsOpen = payload.IsOpen;
        MonsterId = payload.MonsterId;
        PlayerId = payload.PlayerId;
    }
    
    public void Execute()
    {
        OnBattleToggled?.Invoke(IsOpen, PlayerId, MonsterId);
    }
}