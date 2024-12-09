using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPanel : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text battlePowerText;
    [SerializeField] private GameObject turnIndicator;

    private string _playerId;
    
    void OnEnable()
    {
        GameManager.OnGameStateSet += InitializePanel;
        GameState.OnCoinsChanged += UpdateCoins;
        GameState.OnTurnChanged += UpdateTurnIndicator;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InitializePanel;
        GameState.OnCoinsChanged -= UpdateCoins;
        GameState.OnTurnChanged -= UpdateTurnIndicator;
    }

    private void InitializePanel()
    {
        _playerId = GameManager.Instance.GameState.GetOpponentId();
        Player player = GameManager.Instance.GameState.GetPlayer(_playerId);
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{player.ProfilePicture}");
        playerNameText.text = player.Name;
    }
    
    private void UpdateTurnIndicator()
    {
        turnIndicator.SetActive(GameManager.Instance.GameState.IsYourTurn(_playerId));
    }
    
    private void UpdateCoins(string playerId, int coins)
    {
        if (playerId == _playerId)
        {
            coinsText.text = $"Coins: {coins}";
        }
    }
}

