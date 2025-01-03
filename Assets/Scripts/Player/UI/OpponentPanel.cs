using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPanel : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text battlePowerText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject turnIndicator;

    private Player _player;
    
    void OnEnable()
    {
        GameManager.OnGameStateSet += InitializePanel;
        GameState.OnStatsChanged += UpdateStats;
        GameState.OnTurnChanged += UpdateTurnIndicator;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InitializePanel;
        GameState.OnStatsChanged -= UpdateStats;
        GameState.OnTurnChanged -= UpdateTurnIndicator;
    }

    private void InitializePanel()
    {
        _player = GameManager.Instance.GameState.GetOpponent();
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{_player.ProfilePicture}");
        playerNameText.text = _player.Name;
        UpdateStats(_player.Id);
    }
    
    private void UpdateTurnIndicator()
    {
        turnIndicator.SetActive(!GameManager.Instance.GameState.IsYourTurn());
    }
    
    private void UpdateStats(string playerId)
    {
        if (playerId == _player.Id)
        {
            coinsText.text = $"Coins: {_player.Coins}";
            livesText.text = $"Lives: {_player.Lives}";
            battlePowerText.text = $"BP: {_player.BattlePower}";
            levelText.text = $"{_player.Level}";
        }
    }
}

