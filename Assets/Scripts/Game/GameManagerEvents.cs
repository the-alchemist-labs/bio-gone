using System;
using System.Collections.Generic;

public partial class GameManager
{
    public static event Action OnGameStateSet;

    private List<TileId> _selectedTiles;

    private void SetUpEventListeners()
    {
        RollDiceCommand.OnDiceRolled += DiceRolled;
        MovePlayerCommand.OnPlayerMove += MovePlayer;
        ModifyCoinsCommand.OnCoinsModified += CoinsGained;
        NewTurnCommand.OnNewTurn += NewTurn;
        ToggleShopCommand.OnShopToggled += ToggleShop;
        GainItemCommand.OnItemGained += GainItem;
    }

    private void UnsetUpEventListeners()
    {
        RollDiceCommand.OnDiceRolled -= DiceRolled;
        MovePlayerCommand.OnPlayerMove -= MovePlayer;
        ModifyCoinsCommand.OnCoinsModified -= CoinsGained;
        NewTurnCommand.OnNewTurn -= NewTurn;
        ToggleShopCommand.OnShopToggled -= ToggleShop;
        GainItemCommand.OnItemGained -= GainItem;
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

        ;
        GameState.MovePlayer(playerId, newPosition);
    }


    private void CoinsGained(string playerId, int amount)
    {
        GameState.AddCoinsToPlayer(playerId, amount);
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
}