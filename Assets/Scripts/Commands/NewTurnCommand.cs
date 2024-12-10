
using System;
using Newtonsoft.Json;

[Serializable]
public class NewTurnCommandPayload
{ 
    public int PlayerTurnIndex { get; set; }

    public NewTurnCommandPayload(int index)
    {
        PlayerTurnIndex = index;
    }
}

public class NewTurnCommand : ICommand
{
    public static event Action<int> OnNewTurn;

    private int PlayerTurnIndex { get; }
    
    public NewTurnCommand(string payloadString)
    {
        NewTurnCommandPayload payload = JsonConvert.DeserializeObject<NewTurnCommandPayload>(payloadString);
        PlayerTurnIndex = payload.PlayerTurnIndex;
    }
    
    public void Execute()
    {
        OnNewTurn?.Invoke(PlayerTurnIndex);
    }
}