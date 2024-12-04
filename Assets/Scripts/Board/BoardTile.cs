using JetBrains.Annotations;
using UnityEngine;

public enum TileId
{
    None,
    A1,
    A2,
    A3,
    B1,
    B2,
    B3,
    M1,
}
public enum TileZone
{
    BeginningA,
    BeginningB,
    Main,
    Danger,
}

public enum TileType
{
    Coin,
    Exp,
    Curse,
}
public class BoardTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private TileId tileId;
    [SerializeField] private TileZone zone;
    [SerializeField] private TileType type;
    [SerializeField] private TileId tileLeft;
    [SerializeField] private TileId tileRight;
    [SerializeField] private TileId tileAbove;
    [SerializeField] private TileId tileBelow;
    
    
    void Start()
    {
        spriteRenderer.color = GetTileColor(type);
    }

    private Color GetTileColor(TileType tileType)
    {
        switch (tileType)
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

