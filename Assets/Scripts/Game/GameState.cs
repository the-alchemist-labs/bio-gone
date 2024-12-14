using System;
using System.Collections.Generic;
using System.Linq;

public class GameState
{
    public static event Action<string, TileId> OnPlayerMove;
    public static event Action OnTurnChanged;
    public static event Action<int> OnStepsChanged;
    public static event Action<string> OnStatsChanged;
    public static event Action<string> OnPlayerItemsUpdated;
    public static event Action<Player> OnGameOver;

    public string RoomId { get; }
    public List<Player> Players { get; }
    public int PlayerIndexTurn { get; private set; }

    public int Steps { get; private set; }

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
        OnTurnChanged?.Invoke();
    }

    public void MovePlayer(string playerId, TileId newPosition)
    {
        GetPlayer(playerId).MovePlayer(newPosition);
        OnPlayerMove?.Invoke(playerId, newPosition);
    }

    public void AddCoinsToPlayer(string playerId, int modifier)
    {
        GetPlayer(playerId).UpdateCoins(modifier);
        OnStatsChanged?.Invoke(playerId);
    }

    public void UpdatePlayerLive(string playerId, int modifier)
    {
        int lives = GetPlayer(playerId).ModifyLives(modifier);

        OnStatsChanged?.Invoke(playerId);

        if (lives == 0) PlayerDied();
    }

    public void SetSteps(int steps)
    {
        Steps = steps;
        OnStepsChanged?.Invoke(Steps);
    }

    public void AddItemsToPlayer(string playerId, ItemId itemId)
    {
        GetPlayer(playerId).AddItem(itemId);
        OnPlayerItemsUpdated?.Invoke(playerId);

        OnStatsChanged?.Invoke(playerId);
    }

    // TEMP
    public Player GetOpponent()
    {
        return Players.FirstOrDefault(p => p.Id != PlayerProfile.Instance.Id);
    }

    private void PlayerDied()
    {
        List<Player> alivePlayers = Players.Where(player => player.Lives > 0).ToList();
        Player winner = alivePlayers.Count == 1 ? alivePlayers.First() : null;

        if (winner != null) OnGameOver?.Invoke(winner);
    }

    private TileId GetStartPosition(int index)
    {
        return new[] { TileId.A1, TileId.B1 }[index];
    }
}