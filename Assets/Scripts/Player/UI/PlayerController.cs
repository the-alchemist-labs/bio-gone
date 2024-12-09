using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float movementSpeed = 5;
    
    private string _playerId;

    public void SetPlayer(Player player)
    {
        _playerId = player.PlayerId;
        spriteRenderer.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{player.ProfilePicture}");
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
        Debug.Log($"Player {newPosition}");
        if (playerId != _playerId) return;

        Vector3 position = BoardManager.Instance.GetTilePosition(newPosition);
        StartCoroutine(MoveToPosition(position));
    }
    
    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        GameManager.Instance.LandOnTile();
    }
}