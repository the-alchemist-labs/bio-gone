using System;

public static class GameEvents
{
    public static event Action<string, TileId> OnPlayerMove;
    public static event Action OnGameInitialized;
    
    public static void PlayerMove(string playerName, TileId tileId)
    {
        OnPlayerMove?.Invoke(playerName, tileId);
    }
    
    public static void GameInitialized()
    {
        OnGameInitialized?.Invoke();
    }
}
