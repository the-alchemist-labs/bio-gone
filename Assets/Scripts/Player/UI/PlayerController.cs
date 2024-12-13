using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float movementSpeed = 5;
    
    private string _playerId;
    private Coroutine _moveCoroutine;

    public void SetPlayer(Player player)
    {
        _playerId = player.Id;
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
        if (playerId != _playerId) return;

        Vector3 position = BoardManager.Instance.GetTilePosition(newPosition);
        
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine  = StartCoroutine(MoveToPosition(position));
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