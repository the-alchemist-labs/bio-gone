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

    private string _playerId;
    
    void OnEnable()
    {
        GameManager.OnGameStateSet += InitializePanel;
        GameState.OnTurnChanged += UpdateRollButton;
        GameState.OnCoinsChanged += UpdateCoins;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InitializePanel;
        GameState.OnTurnChanged -= UpdateRollButton;
        GameState.OnCoinsChanged -= UpdateCoins;
    }

    private void InitializePanel()
    {
        _playerId = PlayerProfile.Instance.Id;
        Player player = GameManager.Instance.GameState.GetPlayer(_playerId);
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{player.ProfilePicture}");
        playerNameText.text = player.Name;
    }
    
    private void UpdateRollButton()
    {
        rollButton.interactable = GameManager.Instance.GameState.IsYourTurn(_playerId);
    }

    private void UpdateCoins(string playerId, int coins)
    {
        if (playerId == _playerId)
        {
            coinsText.text = $"Coins: {coins}";
        }
    }
    
    public void OnRollClicked()
    {
        rollButton.interactable = false;
        GameManager.Instance.RegisterRollDice();
    }
}

