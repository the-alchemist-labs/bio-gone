using System;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler
{
    private Dictionary<TileType, Action> TileTypeHandlers;

    private const int CoinGain = 5; // get from unity editor
    
    public TileHandler()
    {
        TileTypeHandlers = new Dictionary<TileType, Action>()
        {
            { TileType.Coin, HandleCoinTile },
            { TileType.Shop, HandleShopTile }
        };
    }

    public void Interact(TileType tileType)
    {
        if (TileTypeHandlers.TryGetValue(tileType, out var handler))
        {
            handler();
        }
    }

    private void HandleCoinTile()
    {
        GameManager.Instance.RegisterCoinGain(CoinGain);
        GameManager.Instance.RegisterEndTurn();
    }
    
    private void HandleShopTile()
    {
        GameManager.Instance.RegisterToggleShop(true);
    }
}