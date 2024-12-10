public static class Consts
{
    public const string ServerURI = "192.168.1.223:3000";
    public const int MaxRollValue = 6;
    public const int MinRollValue = 1;
    public const int LevelPowerModifier = 10;
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
