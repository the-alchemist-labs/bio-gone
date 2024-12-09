using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string _playerId;

    public void SetPlayer(Player player)
    {
        _playerId = player.PlayerId;
        // image
    }

    void OnEnable()
    {
        GameState.OnPlayerMove += UpdatePlayerPosition;
    }

    void OnDisable()
    {
        GameState.OnPlayerMove -= UpdatePlayerPosition;
    }

    private void UpdatePlayerPosition(string playerId, TileId newPosition)
    {
        if (playerId != _playerId) return;

        Vector3 position = BoardManager.Instance.GetTilePosition(newPosition);
        gameObject.transform.position = position;
    }
}