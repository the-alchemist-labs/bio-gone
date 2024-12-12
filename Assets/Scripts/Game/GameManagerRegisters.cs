using JetBrains.Annotations;
using Newtonsoft.Json;


public partial class GameManager
{
    public void RegisterRollDice()
    {
        int rollValue = 3; //UnityEngine.Random.Range(Consts.MinRollValue, Consts.MaxRollValue);
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            JsonConvert.SerializeObject(new RollDiceCommandPayload(_player.Id, rollValue))
        ));
    }

    public void RegisterPlayerMove(TileId tileId)
    {
        if (_selectedTiles != null)
        {
            _selectedTiles.ForEach(t => BoardManager.Instance
                .GetTile(t)
                .ToggleSelectableIndicator(false));
            _selectedTiles = null;
        }

        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.MovePlayer,
            JsonConvert.SerializeObject(new MovePlayerCommandPayload(_player.Id, tileId))
        ));
    }

    public void RegisterCoinGain(int amount)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ModifyPlayerCoins,
            JsonConvert.SerializeObject(new ModifyCoinsCommandPayload(_player.Id, amount))
        ));
    }

    public void RegisterEndTurn()
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.NewTurn,
            JsonConvert.SerializeObject(new NewTurnCommandPayload(GameState.GetNextPlayerTurnIndex()))
        ));
    }

    public void RegisterItemGain(ItemId itemId)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.GainItem,
            JsonConvert.SerializeObject(new GainItemCommandPayload(_player.Id, itemId))
        ));
    }

    public void RegisterToggleShop(bool isOpen)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ToggleShop,
            JsonConvert.SerializeObject(new ToggleShopCommandPayload(isOpen))
        ));
    }

    public void RegisterToggleBattle(bool isOpen, MonsterId? monsterId = null)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ToggleBattle,
            JsonConvert.SerializeObject(new ToggleBattleCommandPayload(isOpen, _player.Id, monsterId))
        ));
    }

    public void RegisterBattlePhaseUpdate(BattlePhase phase, BattleItemUsed itemUsed = null)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.UpdateBattlePhase,
            JsonConvert.SerializeObject(new UpdateBattlePhaseCommandPayload(phase, itemUsed))
        ));
    }
}