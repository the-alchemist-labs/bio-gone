using System;
using System.Collections.Generic;
using UnityEngine;

public class TileService
{
    private readonly Dictionary<TileType, Action> _tileTypeHandlers;
    
    public TileService()
    {
        _tileTypeHandlers = new Dictionary<TileType, Action>()
        {
            { TileType.Coin, HandleCoinTile },
            { TileType.Shop, HandleShopTile },
            { TileType.Monster, HandleMonsterTile },
            { TileType.Exp, HandleExpTile },
            { TileType.Guardian, HandleGuardianTile },

        };
    }

    public void Interact(TileType tileType)
    {
        if (_tileTypeHandlers.TryGetValue(tileType, out var handler))
        {
            handler();
        }
    }

    private void HandleCoinTile()
    {
        GameManager.Instance.RegisterCoinsUpdate(Consts.TileCoinGain);
    }
    
    private void HandleShopTile()
    {
        GameManager.Instance.RegisterToggleShop(true);
    }
    
    private void HandleMonsterTile()
    {
        Player player = GameManager.Instance.GameState.GetPlayer();
        MonsterId monsterId = Battle.GetMonster(player.Level);
        GameManager.Instance.RegisterToggleBattle(true, monsterId);
    }

    private void HandleExpTile()
    {
        GameManager.Instance.RegisterExpUpdate(Consts.TileExpGain);
    }
    
    private void HandleGuardianTile()
    {
        GameManager.Instance.RegisterToggleBattle(true, MonsterId.Guardian);
    }
}