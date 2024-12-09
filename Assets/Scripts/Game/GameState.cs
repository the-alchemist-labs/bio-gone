using System;
using System.Collections.Generic;

public class GameState
{
    public static event Action<string, TileId> OnPlayerMove;
    public static event Action<bool> OnTurnChanged;
    public static event Action<int> OnStepsChanged;

    public static event Action<string, int> OnCoinsChanged;

    public string RoomId { get; }
    public List<Player> Players {  get; }
    public int PlayerIndexTurn { get; private set; }
    
    public int Steps { get; private set; }

    private string _playerId;

    public GameState(MatchFoundEvent matchFoundEvent)
    {
        _playerId = PlayerProfile.Instance.Id;
        RoomId = matchFoundEvent.RoomId;
        Players = matchFoundEvent.PlayersData;
        PlayerIndexTurn = matchFoundEvent.FirstTurnPlayer;
        OnTurnChanged?.Invoke(IsYourTurn(_playerId));
    }

    public Player GetPlayer(string id)
    {
        return Players.Find(player => player.PlayerId == id);
    }

    public int GetNextPlayerTurnIndex()
    {
        return (PlayerIndexTurn + 1) % Players.Count;
    }
    
    public bool IsYourTurn(string yourPlayerId)
    {
        return Players[PlayerIndexTurn].PlayerId == yourPlayerId;
    }

    public void UpdatePlayerTurn(int index)
    {
        PlayerIndexTurn = index;
        OnTurnChanged?.Invoke(IsYourTurn(_playerId));
    }
    
    public void MovePlayer(string playerId, TileId newPosition)
    {
        GetPlayer(playerId).MovePlayer(newPosition);
        OnPlayerMove?.Invoke(playerId, newPosition);
    }

    public void AddCoinsToPlayer(string playerId, int amount)
    {
        GetPlayer(playerId).UpdateCoins(amount);
        OnCoinsChanged?.Invoke(playerId, amount);
    }

    public void SetSteps(int steps)
    {
        Steps = steps;
        OnStepsChanged?.Invoke(Steps);
    }
}