using System.Collections.Generic;
using UnityEngine;

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
        _isSelectable = isToggledOn;
        selectableIndicator.enabled = isToggledOn;
    }

    public void TileSelected()
    {
        Debug.Log("Tile selected");
        GameManager.Instance.RegisterPlayerMove(tileId);
    }

    void OnMouseDown()
    {
        if (_isSelectable)
        {
            GameManager.Instance.RegisterPlayerMove(tileId);
        }
    }

    private Color GetTileColor(TileType type)
    {
        switch (type)
        {
            case TileType.Coin:
                return new Color32(50, 109, 168, 255);
            case TileType.Exp:
                return new Color32(41, 217, 179, 255);
            case TileType.Curse:
                return new Color32(110, 125, 107, 255);
            case TileType.Shop:
                return new Color32(79, 158, 66, 255);
            case TileType.Monster:
                return new Color32(170, 50, 50, 255);
            case TileType.Guardian:
                return new Color32(242, 208, 24, 255);
            default:
                return Color.white;
        }
    }
}