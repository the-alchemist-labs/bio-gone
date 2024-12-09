using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer tile;
    [SerializeField] private SpriteRenderer selectableIndicator;

    [SerializeField] public TileId tileId;
    [SerializeField] public TileType tileType;
    [SerializeField] private TileId nextTile;
    [SerializeField] private TileId nexTile2;
    [SerializeField] private TileZone zone;

    private bool _isSelectable = false;

    void Start()
    {
        tile.color = GetTileColor(tileType);
    }


    public List<TileId> GetNextTiles()
    {
        return nexTile2 == TileId.None
            ? new List<TileId> { nextTile }
            : new List<TileId> { nextTile, nexTile2 };
    }

    public void ToggleSelectableIndicator(bool isToggledOn)
    {
        selectableIndicator.enabled = isToggledOn;
        _isSelectable = isToggledOn;
    }

    public void TileSelected()
    {
        Debug.Log("Tile selected");
        GameManager.Instance.RegisterPlayerMove(tileId);
    }

    // add colider to work
    void OnMouseDown()
    {
        if (_isSelectable)
        {
            Debug.Log("Circle clicked!");
            GameManager.Instance.RegisterPlayerMove(tileId);
        }
    }

    private Color GetTileColor(TileType type)
    {
        switch (type)
        {
            case TileType.Coin:
                return Color.blue;
            case TileType.Exp:
                return Color.red;
            case TileType.Curse:
                return Color.gray;
            default:
                return Color.white;
        }
    }
}