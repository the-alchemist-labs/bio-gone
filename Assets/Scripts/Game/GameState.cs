using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class GameState
{
    public static event Action<string, TileId> OnPlayerMove;
    public static event Action OnTurnChanged;
    public static event Action<int?> OnStepsChanged;
    public static event Action<string> OnStatsChanged;
    public static event Action<int> OnTimerUpdated;

    public string RoomId { get; }
    public List<Player> Players { get; }
    public int PlayerIndexTurn { get; private set; }

    public int? Steps { get; private set; }

    public int TurnTimer { get; private set; }

    private string _playerId;

    public GameState(MatchFoundEvent matchFoundEvent)
    {
        _playerId = PlayerProfile.Instance.Id;

        RoomId = matchFoundEvent.RoomId;
        PlayerIndexTurn = matchFoundEvent.FirstTurnPlayer;
        Players = new List<Player>();

        for (int i = 0; i < matchFoundEvent.PlayersData.Count; i++)
        {
            PlayerData playerData = matchFoundEvent.PlayersData[i];
            Players.Add(new Player(
                playerData.Id,
                playerData.Name,
                playerData.ProfilePicture,
                GetStartPosition(i)
            ));
        }
    }

    public Player GetPlayer(string id = null)
    {
        string playerId = id ?? PlayerProfile.Instance.Id;
        return Players.Find(player => player.Id == playerId);
    }

    public int GetNextPlayerTurnIndex()
    {
        return (PlayerIndexTurn + 1) % Players.Count;
    }

    public bool IsYourTurn(string id = null)
    {
        string playerId = id ?? PlayerProfile.Instance.Id;
        return Players[PlayerIndexTurn].Id == playerId;
    }

    public void UpdatePlayerTurn(int index)
    {
        PlayerIndexTurn = index;
        SetTimer(Consts.TurnSeconds);
        OnTurnChanged?.Invoke();
    }

    public void SetTimer(int timer)
    {
        if (timer <= 0 && IsYourTurn())
        {
            GameManager.Instance.RegisterEndTurn();
        }
        
        TurnTimer = timer;
        OnTimerUpdated?.Invoke(TurnTimer);
    }

    public void MovePlayer(string playerId, TileId newPosition)
    {
        GetPlayer(playerId).MovePlayer(newPosition);
        OnPlayerMove?.Invoke(playerId, newPosition);
    }

    public void ModifyPlayerCoins(string playerId, int modifier)
    {
        GetPlayer(playerId).UpdateCoins(modifier);
        OnStatsChanged?.Invoke(playerId);
    }

    public void AddExpToPlayer(string playerId, int amount)
    {
        Player player = GetPlayer(playerId);
        player.AddExp(amount);

        if (player.ShouldLevelUp() && !player.IsMaxLevel())
        {
            player.LevelUp();
            SoundManager.Instance.PlaySound(SoundId.LevelUp);
        }

        OnStatsChanged?.Invoke(player.Id);
    }

    public void UpdatePlayerLive(string playerId, int modifier)
    {
        int lives = GetPlayer(playerId).ModifyLives(modifier);

        if (modifier < 0)  SoundManager.Instance.PlaySound(SoundId.LoseLife);
        if (modifier > 0) SoundManager.Instance.PlaySound(SoundId.GainLife);

        OnStatsChanged?.Invoke(playerId);

        if (lives == 0) PlayerDied();
    }

    public void SetSteps(int? steps)
    {
        Steps = steps;
        OnStepsChanged?.Invoke(Steps);
    }

    public void UpdatePlayerInventory(string playerId, ItemId itemId, ItemAction action)
    {
        GetPlayer(playerId).UpdateInventory(itemId, action);
        OnStatsChanged?.Invoke(playerId);
    }

    // On 2 players
    public Player GetOpponent()
    {
        return Players.FirstOrDefault(p => p.Id != PlayerProfile.Instance.Id);
    }

    private void PlayerDied()
    {
        List<Player> alivePlayers = Players.Where(player => player.Lives > 0).ToList();
        Player winner = alivePlayers.Count == 1 ? alivePlayers.First() : null;

        if (winner != null) GameManager.Instance.RegisterGameOver(winner.Id);
    }

    private TileId GetStartPosition(int index)
    {
        return new[] { TileId.A1, TileId.B1 }[index];
    }
}