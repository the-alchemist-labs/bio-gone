using Newtonsoft.Json;
using Unity.VisualScripting;

public partial class GameManager
{
    public void RegisterRollDice(int rollValue)
    {
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

    public void RegisterCoinsUpdate(int amount)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ModifyPlayerCoins,
            JsonConvert.SerializeObject(new ModifyCoinsCommandPayload(_player.Id, amount))
        ));
    }
    
    public void RegisterExpUpdate(int amount)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ModifyPlayerExp,
            JsonConvert.SerializeObject(new ModifyExpCommanddPayload(_player.Id, amount))
        ));
    }

    public void RegisterLivesUpdate(int amount)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ModifyPlayerLive,
            JsonConvert.SerializeObject(new ModifyLiveCommandPayload(_player.Id, amount))
        ));
    }
    
    public void RegisterEndTurn()
    {
        ClearTurn();
        int playerIndex = GameState.GetNextPlayerTurnIndex();
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.NewTurn,
            JsonConvert.SerializeObject(new NewTurnCommandPayload(playerIndex))
        ));
    }

    public void RegisterInventoryUpdate(ItemId itemId, ItemAction action)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.GainItem,
            JsonConvert.SerializeObject(new UpdateInventoryCommandPayload(_player.Id, itemId, action))
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

    
    public void RegisterBattlePhaseUpdate(BattlePhase phase, BattleItemUsed itemUsed = null, FleeBattle fleeBattle = null)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.UpdateBattlePhase,
            JsonConvert.SerializeObject(new UpdateBattlePhaseCommandPayload(phase, itemUsed, fleeBattle))
        ));
    }

}