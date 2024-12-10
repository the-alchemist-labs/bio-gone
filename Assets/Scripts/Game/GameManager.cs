using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    private Commander Commander { get; set; }

    private string _playerId;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _playerId = PlayerProfile.Instance.Id;
        Commander = new Commander();
        GameState = new GameState(MatchFoundResults.Instance);
        OnGameStateSet?.Invoke();
        
        GameState.UpdatePlayerTurn(GameState.PlayerIndexTurn);
    }

    void OnEnable()
    {
        SetUpEventListeners();
    }

    void OnDisable()
    {
        UnsetUpEventListeners();
    }

    public void TakeStep()
    {
        if (IsLastStep())
        {
            RegisterEndTurn();
        }
        
        TileId currentPosition = GameState.GetPlayer(_playerId).Position;
        List<TileId> nextTiles = BoardManager.Instance.GetTile(currentPosition).GetNextTiles();

        if (nextTiles.Count == 1)
        {
            RegisterPlayerMove(nextTiles.First());
            return;
        }

        _selectedTiles = nextTiles;
        _selectedTiles.ForEach(t => BoardManager.Instance
            .GetTile(t)
            .ToggleSelectableIndicator(true));
    }
    
    public void LandOnTile()
    {
        if (!GameState.IsYourTurn()) return;
        
        TileId position = GameState.GetPlayer(_playerId).Position;
        TileType tileType = BoardManager.Instance.GetTile(position).tileType;
        
        if (IsLastStep())
        {
            BoardManager.Instance.InteractWithTile(tileType);
            return;
        }
        
        if (BoardManager.IntractableOnPassTileTypes.Contains(tileType))
        {
            BoardManager.Instance.InteractWithTile(tileType);
            return;
        }
        
        TakeStep();
    }
    
    public void RegisterRollDice()
    {
        int rollValue = 3; //UnityEngine.Random.Range(Consts.MinRollValue, Consts.MaxRollValue);
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            JsonConvert.SerializeObject(new RollDiceCommandPayload(_playerId, rollValue))
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
            JsonConvert.SerializeObject(new MovePlayerCommandPayload(_playerId, tileId))
        ));
    }

    public void RegisterCoinGain(int amount)
    {
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.ModifyPlayerCoins,
            JsonConvert.SerializeObject(new ModifyCoinsCommandPayload(_playerId, amount))
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
            JsonConvert.SerializeObject(new GainItemCommandPayload(_playerId, itemId))
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
    
    private bool IsLastStep()
    {
        return GameState.Steps == 0;
    }
}