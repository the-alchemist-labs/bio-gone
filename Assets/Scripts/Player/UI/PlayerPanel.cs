using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        _playerId = PlayerProfile.Instance.Id;
    }
    
    void OnEnable()
    {
        GameState.OnTurnChanged += UpdateRollButton;
        GameState.OnCoinsChanged += UpdateCoins;
    }

    void OnDisable()
    {
        GameState.OnTurnChanged -= UpdateRollButton;
        GameState.OnCoinsChanged -= UpdateCoins;
    }
    
    private void UpdateRollButton(bool yourTurn)
    {
        rollButton.interactable = yourTurn;
    }

    private void UpdateCoins(string playerId, int coins)
    {
        if (playerId == _playerId)
        {
            playerNameText.text = $"Coins: {coins}";
        }
    }
    
    public void OnRollClicked()
    {
        GameManager.Instance.RegisterRollDice();
    }
}

