
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class GameOverCommandPayload
{ 
    public string PlayerId { get; set;  }
    
    public GameOverCommandPayload(string playerId)
    {
        PlayerId = playerId;
    }
}

public class GameOverCommand : ICommand
{
    public static event Action<string> OnGameOver;

    private string PlayerId { get; }
    
    public GameOverCommand(string payloadString)
    {
        GameOverCommandPayload payload = JsonConvert.DeserializeObject<GameOverCommandPayload>(payloadString);
        PlayerId = payload.PlayerId;
    }
    
    public void Execute()
    {
        OnGameOver?.Invoke(PlayerId);
    }
}