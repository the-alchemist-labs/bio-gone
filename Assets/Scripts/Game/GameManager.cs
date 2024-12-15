using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    public Battle Battle { get; private set; }
    private Commander Commander { get; set; }

    private Player _player;
    
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
        Commander = new Commander();
        GameState = new GameState(MatchFoundResults.Instance);
        _player = GameState.GetPlayer();
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
            return;
        }
        
        TileId currentPosition = GameState.GetPlayer(_player.Id).Position;
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
        
        TileId position = GameState.GetPlayer(_player.Id).Position;
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
    
    private bool IsLastStep()
    {
        return GameState.Steps == 0;
    }
}