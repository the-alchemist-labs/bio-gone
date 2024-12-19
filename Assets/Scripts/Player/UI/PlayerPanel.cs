using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text battlePowerText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private LivesContainer livesContainer;
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
        _player = isPlayer ? GameManager.Instance.GameState.GetPlayer() : GameManager.Instance.GameState.GetOpponent();
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{_player.ProfilePicture}");
        playerNameText.text = _player.Name;
        UpdateStats(_player.Id);
    }

    private void UpdateStats(string playerId)
    {
        if (playerId == _player.Id)
        {
            livesContainer.UpdateLives(_player.Lives);
            coinsText.text = $"{_player.Coins}";
            battlePowerText.text = $"{_player.BattlePower}";
            levelText.text = $"{_player.Level}";
            expSlider.value = (float) _player.Experience / Consts.ExpToLevelUp;
        }
    }

    private void UpdateTurnIndicator()
    {
        turnIndicator.SetActive(GameManager.Instance.GameState.IsYourTurn(_player.Id));
    }
}

