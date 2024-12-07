using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public static BoardManager Instance { get; private set; }
    private Dictionary<TileId, Vector3> TilePositionMap { get; set; }

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
        TilePositionMap ??= MapTilePositions();
    }
    
    void OnEnable()
    {
        GameManager.OnGameStateSet += InstantiateBoard;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InstantiateBoard;
    }
    
    private void InstantiateBoard()
    {
        TilePositionMap ??= MapTilePositions();
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
    
    public Vector3 GetTilePosition(TileId tileId)
    {
        return TilePositionMap.TryGetValue(tileId, out var position)
            ? position
            : throw new KeyNotFoundException($"TileId {tileId} does not exist in the map.");
    }

    private Dictionary<TileId, Vector3> MapTilePositions()
    {
        Dictionary<TileId, Vector3> tileMap = new Dictionary<TileId, Vector3>();
        foreach (BoardTile tile in GetComponentsInChildren<BoardTile>())
        {
            tileMap[tile.TileId] = tile.transform.position;
        }

        return tileMap;
    }
}