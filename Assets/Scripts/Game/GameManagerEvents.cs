using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameManager
{
    public static event Action OnGameStateSet;

    private List<TileId> _selectedTiles;

    private void SetUpEventListeners()
    {
        RollDiceCommand.OnDiceRolled += DiceRolled;
        MovePlayerCommand.OnPlayerMove += MovePlayer;
        GainCoinsCommand.OnCoinsGained += CoinsGained;
        NewTurnCommand.OnNewTurn += NewTurn;
    }

    private void UnsetUpEventListeners()
    {
        RollDiceCommand.OnDiceRolled -= DiceRolled;
        MovePlayerCommand.OnPlayerMove -= MovePlayer;
        GainCoinsCommand.OnCoinsGained -= CoinsGained;
        NewTurnCommand.OnNewTurn -= NewTurn;
    }
    private void DiceRolled(string playerId, int steps)
    {
        // show animation of roll with the result

        if (playerId == PlayerProfile.Instance.Id)
        {
            GameState.SetSteps(steps);
            TakeStep();
        }
    }

    private void TakeStep()
    {
        if (IsLastStep())
        {
            RegisterEndTurn();
        }
        
        TileId currentPosition = GameState.GetPlayer(_playerId).Position;
        List<TileId> nextTiles = BoardManager.Instance.GetTile(currentPosition).GetNextTiles();

        if (nextTiles.Count == 1)
        {
            RegisterPlayerMove(nextTiles.First());
            return;
        }

        _selectedTiles = nextTiles;
        _selectedTiles.ForEach(t => BoardManager.Instance
            .GetTile(t)
            .ToggleSelectableIndicator(true));
    }

    private void MovePlayer(string playerId, TileId newPosition)
    {
        if (GameState.IsYourTurn(_playerId))
        {
            GameState.SetSteps(GameState.Steps - 1);
        };
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
    
    private bool IsLastStep()
    {
        return GameState.Steps == 0;
    }
}