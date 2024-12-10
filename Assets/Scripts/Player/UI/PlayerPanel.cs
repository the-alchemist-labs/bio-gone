using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text battlePowerText;
    [SerializeField] private Button rollButton;

    private Player _player;
    
    void OnEnable()
    {
        
        GameManager.OnGameStateSet += InitializePanel;
        GameState.OnTurnChanged += UpdateRollButton;
        GameState.OnCoinsChanged += UpdateCoins;
        GameState.OnPlayerItemsUpdated += ItemsUpdated;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InitializePanel;
        GameState.OnTurnChanged -= UpdateRollButton;
        GameState.OnCoinsChanged -= UpdateCoins;
        GameState.OnPlayerItemsUpdated -= ItemsUpdated;

    }

    private void InitializePanel()
    {
        _player = GameManager.Instance.GameState.GetPlayer(PlayerProfile.Instance.Id);
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{_player.ProfilePicture}");
        playerNameText.text = _player.Name;
    }
    
    private void UpdateRollButton()
    {
        rollButton.interactable = GameManager.Instance.GameState.IsYourTurn();
    }

    private void UpdateCoins(string playerId, int coins)
    {
        if (playerId == _player.Id)
        {
            coinsText.text = $"Coins: {coins}";
        }
    }

    private void ItemsUpdated(string playerId)
    {
        if (playerId == _player.Id)
        {
            Debug.Log($"Player {_player.Id} Items: { GameManager.Instance.GameState.GetPlayer(_player.Id).Items.Count })");
        }
    }
    
    public void OnRollClicked()
    {
        rollButton.interactable = false;
        GameManager.Instance.RegisterRollDice();
    }
    
}

