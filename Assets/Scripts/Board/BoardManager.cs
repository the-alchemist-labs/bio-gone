using System;
using System.Collections.Generic;
using UnityEngine;

public partial class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public static BoardManager Instance { get; private set; }
    
    public TileHandler TileHandler { get; private set; }
    private Dictionary<TileId, BoardTile> TilesMap { get; set; }

    public static readonly List<TileType> IntractableOnPassTileTypes = new List<TileType>
    {
        TileType.Shop,
        TileType.Guardian,
        TileType.Monster,
        TileType.Goblet,
    };
    
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
        TilesMap ??= MapTiles();
        TileHandler = new TileHandler();
    }
    
    void OnEnable()
    {
        GameState.OnGameStateSet += InstantiateBoard;
    }

    void OnDisable()
    {
        GameState.OnGameStateSet -= InstantiateBoard;
    }
    
    public BoardTile GetTile(TileId tileId)
    {
        return TilesMap.TryGetValue(tileId, out var tile)
            ? tile
            : throw new KeyNotFoundException($"TileId {tileId} does not exist in the map.");
    }
    
    public Vector3 GetTilePosition(TileId tileId)
    {
        return TilesMap.TryGetValue(tileId, out var tile)
            ? tile.transform.position
            : throw new KeyNotFoundException($"TileId {tileId} does not exist in the map.");
    }

    public void InteractWithTile(TileType tileType)
    {
        TileHandler.Interact(tileType);
    }
    
    private void InstantiateBoard()
    {
        TilesMap ??= MapTiles();
        foreach (Player player in GameManager.Instance.GameState.Players)
        {
            GameObject newPlayer = Instantiate(
                playerPrefab,
                GetTilePosition(player.Position),
                Quaternion.identity,
                gameObject.transform
            );
            PlayerController controller = newPlayer.GetComponent<PlayerController>();
            controller.SetPlayer(player);
        }
    }
    
    private Dictionary<TileId, BoardTile> MapTiles()
    {
        Dictionary<TileId, BoardTile> tileMap = new Dictionary<TileId, BoardTile>();
        foreach (BoardTile tile in GetComponentsInChildren<BoardTile>())
        {
            tileMap[tile.tileId] = tile;
        }

        return tileMap;
    }
}