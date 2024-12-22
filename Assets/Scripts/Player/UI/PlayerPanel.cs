using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
    [SerializeField] [CanBeNull] private Button rollButton;
    [SerializeField] [CanBeNull] private GameObject rollButtonMask;
    [SerializeField] private Animator levelUpAnimation;

    private Player _player;

    void OnEnable()
    {
        GameManager.OnGameStateSet += InitializePanel;
        GameState.OnStatsChanged += UpdateStats;
        GameState.OnTurnChanged += UpdateTurnIndicator;
        GameState.OnLevelUp += OnLevelUp;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InitializePanel;
        GameState.OnStatsChanged -= UpdateStats;
        GameState.OnTurnChanged -= UpdateTurnIndicator;
        GameState.OnLevelUp -= OnLevelUp;
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
            StartCoroutine(GradualExpIncrease((float)_player.GetSliderExperience() / Consts.ExpToLevelUp));
        }
    }

    private void UpdateTurnIndicator()
    {
        bool yourTurn = GameManager.Instance.GameState.IsYourTurn(_player.Id);
        turnIndicator.SetActive(yourTurn);
        if (rollButtonMask != null) rollButtonMask.gameObject.SetActive(!yourTurn);
        if (rollButton != null) rollButton.interactable = yourTurn;
    }

    public void OnRollClicked()
    {
        int rollValue = 3; // Random.Range(Consts.MinRollValue, Consts.MaxRollValue);
        GameManager.Instance.RegisterRollDice(rollValue);
        if (rollButtonMask != null) rollButtonMask.gameObject.SetActive(true);
        if (rollButton != null) rollButton.interactable = false;
    }

    private void OnLevelUp(string playerId)
    {
        if (playerId == _player.Id)
        {
            levelUpAnimation.SetTrigger("LevelUpTrigger");
            expSlider.value = (float)_player.GetSliderExperience() / Consts.ExpToLevelUp;
        }
    }
    
    private IEnumerator GradualExpIncrease(float targetValue)
    {
        while (expSlider.value < targetValue)
        {
            expSlider.value += Time.deltaTime * 0.3f;
            yield return null;
        }
    
        expSlider.value = targetValue;
    }
}