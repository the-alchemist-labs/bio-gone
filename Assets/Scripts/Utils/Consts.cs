public static class Consts
{
    public const string ServerURI = "http://192.168.1.223:3000";
    // public const string ServerURI = "https://munch-party.onrender.com";
    public const int DefaultLives = 3;
    public const int LevelPowerModifier = 10;
    public const int MonsterLevelDiff = 2;
    public const int MaxLevel = 10;
    public const int ExpToLevelUp = 250;
    public const int TileCoinGain = 100;
    public const int TileExpGain = 150;
    public const float TileYPositionCorrection = -0.04f;
    public const int TurnSeconds = 50;
    public const int InterruptSeconds = 5;
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
    public const string MainMenu = "MainMenuScene";
    public const string Matchmaking = "MatchmakingScene";
    public const string Game = "GameScene";
}

public static class PlayerPrefKeys
{
    public const string BGM_Volume = "BGM_Volume";
    public const string SFX_Volume = "SFX_Volume";
}
