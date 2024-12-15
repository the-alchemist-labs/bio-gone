using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum Command
{
    NewTurn,
    RollDice,
    MovePlayer,
    ModifyPlayerCoins,
    GainItem,
    ToggleShop,
    ToggleBattle,
    UpdateBattlePhase,
    ModifyPlayerLive,
    ModifyPlayerExp,
}

public interface ICommand
{
    void Execute();
}

[Serializable]
public class CommandEvent
{
    public string RoomId { get; set; }
    public Command CommandType { get; set; }
    public string Payload { get; set; }

    public CommandEvent(string roomId, Command commandType, string payload)
    {
        RoomId = roomId;
        CommandType = commandType;
        Payload = payload;
    }
}