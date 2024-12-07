using UnityEngine;

public class BoardTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] public TileId TileId;
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

