using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    public Battle Battle { get; private set; }

    [SerializeField] private RewardAnimationPool rewardAnimationPool;

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
        if (GameState.IsYourTurn() && IsLastStep())
        {
            //RegisterExpUpdate(Consts.TileLandExpGain);
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
        }

        if (BoardManager.IntractableOnPassTileTypes.Contains(tileType))
        {
            BoardManager.Instance.InteractWithTile(tileType);
            return;
        }

        TakeStep();
    }

    public void DisplayRewards(RewardType rewardType, RewardDestination rewardDest, int rewardAmount)
    {
        int count = rewardAmount / 20;
        StartCoroutine(DisplayRewardsWithDelay(rewardType, rewardDest, count));
    }

    
    private IEnumerator DisplayRewardsWithDelay(RewardType rewardType, RewardDestination rewardDest, int count)
    {
        for (int i = 0; i < count; i++)
        {
            rewardAnimationPool.DisplayRewardToken(rewardType, rewardDest);
            yield return new WaitForSeconds(0.03f);
        }
    }
    private bool IsLastStep()
    {
        return GameState.Steps == 0;
    }
}