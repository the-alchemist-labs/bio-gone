public static class Consts
{
    public const string ServerURI = "192.168.1.223:3000";
    public const int DefaultLives = 5;
    public const int MaxRollValue = 6;
    public const int MinRollValue = 1;
    public const int LevelPowerModifier = 10;
    public const int MonsterLevelDiff = 2;
    public const int MaxLevel = 10;
    public const int ExpToLevelUp = 200;
    public const int TileCoinGain = 100;
    public const int TileExpGain = 150;

}

public static class SocketEvents
{
    public const string CommandReceived = "commandReceived";
    public const string PostCommand = "postCommand";
    public const string SearchMatch = "searchMatch";
    public const string MatchFound = "matchFound";
}

public static class SceneNames
{
    public const string Loading = "LoadingScene";
    public const string MainMenu = "MainMenuScene";
    public const string Matchmaking = "MatchmakingScene";
    public const string Game = "GameScene";
    
}
