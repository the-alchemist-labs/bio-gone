using System;
using System.Collections.Generic;
using UnityEngine;

public class TileService
{
    private Dictionary<TileType, Action> _tileTypeHandlers;

    private const int CoinGain = 5; // get from unity editor
    
    public TileService()
    {
        _tileTypeHandlers = new Dictionary<TileType, Action>()
        {
            { TileType.Coin, HandleCoinTile },
            { TileType.Shop, HandleShopTile },
            { TileType.Monster, HandleMonsterTile }
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
        GameManager.Instance.RegisterCoinsUpdate(CoinGain);
        GameManager.Instance.RegisterEndTurn();
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
}