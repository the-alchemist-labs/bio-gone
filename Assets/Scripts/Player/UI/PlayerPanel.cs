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

            bool hasLevelUp = int.Parse(levelText.text) < _player.Level;
            livesContainer.UpdateLives(_player.Lives);
            coinsText.text = $"{_player.Coins}";

            if (_player.BattlePower > int.Parse(battlePowerText.text)) StartCoroutine(HighlightText(Color.red, 0.5F));
            battlePowerText.text = $"{_player.BattlePower}";

            float sliderValue = (float)_player.GetSliderExperience() / Consts.ExpToLevelUp;
            StartCoroutine(GradualExpIncrease(sliderValue, hasLevelUp));
        }
    }

    private void UpdateTurnIndicator()
    {
        bool yourTurn = GameManager.Instance.GameState.IsYourTurn(_player.Id);
        turnIndicator.SetActive(yourTurn);
        if (rollButtonMask != null) rollButtonMask.gameObject.SetActive(!yourTurn);
        if (rollButton != null) rollButton.interactable = yourTurn;
    }
    
    private IEnumerator HighlightText(Color color, float duration)
    {
        Color originalColor = playerImage.color;
        battlePowerText.color = color;
        yield return new WaitForSeconds(duration);
        battlePowerText.color = originalColor;
    }
    
    public void OnRollClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        int rollValue = 3;
        GameManager.Instance.RegisterRollDice(rollValue);
        if (rollButtonMask != null) rollButtonMask.gameObject.SetActive(true);
        if (rollButton != null) rollButton.interactable = false;
    }
    
    private IEnumerator GradualExpIncrease(float targetValue, bool hasLeveledUp)
    {
        bool shouldLevelUp = hasLeveledUp;
        
        yield return new WaitForSeconds(0.4f);

        while (expSlider.value < targetValue || shouldLevelUp)
        {
            expSlider.value += Time.deltaTime;
            if (expSlider.value >= expSlider.maxValue)
            {
                levelUpAnimation.SetTrigger("LevelUpTrigger");
                levelText.text = $"{_player.Level}";
                expSlider.value = 0;
                shouldLevelUp = false;
            }
            
            yield return null;
        }
    
        //expSlider.value = targetValue;
    }
}