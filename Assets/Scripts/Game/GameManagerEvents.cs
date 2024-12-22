using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public partial class GameManager
{
    public static event Action OnGameStateSet;

    private List<TileId> _selectedTiles;

    private void SetUpEventListeners()
    {
        RollDiceCommand.OnDiceRolled += DiceRolled;
        MovePlayerCommand.OnPlayerMove += MovePlayer;
        ModifyCoinsCommand.OnCoinsModified += CoinsGained;
        ModifyLiveCommand.OnLivesModified += LivesUpdated;
        NewTurnCommand.OnNewTurn += NewTurn;
        ToggleShopCommand.OnShopToggled += ToggleShop;
        GainItemCommand.OnItemGained += GainItem;
        ToggleBattleCommand.OnBattleToggled += ToggleBattle;
        UpdateBattlePhaseCommand.OnBattlePhaseChanged += BattlePhaseChanged;
        GameState.OnGameOver += GameOver;
        ModifyExpCommand.OnExpUpdated += ExpUpdated;
    }

    private void UnsetUpEventListeners()
    {
        RollDiceCommand.OnDiceRolled -= DiceRolled;
        MovePlayerCommand.OnPlayerMove -= MovePlayer;
        ModifyCoinsCommand.OnCoinsModified -= CoinsGained;
        ModifyLiveCommand.OnLivesModified -= LivesUpdated;
        NewTurnCommand.OnNewTurn -= NewTurn;
        ToggleShopCommand.OnShopToggled -= ToggleShop;
        GainItemCommand.OnItemGained -= GainItem;
        ToggleBattleCommand.OnBattleToggled -= ToggleBattle;
        UpdateBattlePhaseCommand.OnBattlePhaseChanged -= BattlePhaseChanged;
        GameState.OnGameOver -= GameOver;
        ModifyExpCommand.OnExpUpdated -= ExpUpdated;
    }

    private void DiceRolled(string playerId, int steps)
    {
        PopupManager.Instance.dicePopup.Display(steps);

        if (playerId == PlayerProfile.Instance.Id)
        {
            GameState.SetSteps(steps);
        }
    }

    private void MovePlayer(string playerId, TileId newPosition)
    {
        if (GameState.IsYourTurn())
        {
            GameState.SetSteps(GameState.Steps - 1);
        }

        GameState.MovePlayer(playerId, newPosition);
    }
    
    private void CoinsGained(string playerId, int modifier)
    {
        RewardDestination dest = playerId == _player.Id ? RewardDestination.Player : RewardDestination.Opponent;
        if (modifier > 0) DisplayRewards(RewardType.Coin, dest, modifier);
        GameState.ModifyPlayerCoins(playerId, modifier);
    }

    private void ExpUpdated(string playerId, int amount)
    {
        RewardDestination dest = playerId == _player.Id ? RewardDestination.Player : RewardDestination.Opponent;
        DisplayRewards(RewardType.Exp, dest, amount);
        Instance.GameState.AddExpToPlayer(playerId, amount);
    }
    
    private void LivesUpdated(string playerId, int modifier)
    {
        GameState.UpdatePlayerLive(playerId, modifier);
    }
    
    private void NewTurn(int index)
    {
        GameState.UpdatePlayerTurn(index);
    }

    private void ToggleShop(bool isOpen)
    {
        ShopPopup shop = PopupManager.Instance.shopPopup;
        if (isOpen) shop.Display();
        else shop.ClosePopup();
    }

    private void GainItem(string playerId, ItemId itemId)
    {
        GameState.AddItemsToPlayer(playerId, itemId);
    }

    private void ToggleBattle(bool isOpen, string playerId, MonsterId? monsterId)
    {
        BattlePopup battle = PopupManager.Instance.battlePopup;
        if (isOpen && monsterId.HasValue)
        {
            Battle = new Battle(playerId, monsterId.Value);
            battle.Display(Battle);
        }
            
        else battle.ClosePopup();
    }

    private void BattlePhaseChanged(BattlePhase phase, [CanBeNull] BattleItemUsed usedItem, bool? hasEscaped)
    {
        Battle.UpdateBattlePhase(phase, hasEscaped);
        if (usedItem != null)
        {
            Battle.UseItem(usedItem);
        }
    }

    private void GameOver(Player player)
    {
        PopupManager.Instance.gameOverPopup.Display(player);
    }
}