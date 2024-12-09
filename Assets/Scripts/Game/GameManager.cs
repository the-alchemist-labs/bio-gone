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

        // show animation for who is starting
    }

    void OnEnable()
    {
        SetUpEventListeners();
    }

    void OnDisable()
    {
        UnsetUpEventListeners();
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
            Command.GainCoins,
            JsonConvert.SerializeObject(new GainCoinsCommandPayload(_playerId, amount))
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
}