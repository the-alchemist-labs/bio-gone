using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text battlePowerText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button rollButton;
    [SerializeField] private Button bagButton;
    [SerializeField] private TMP_Text bagText;
    [SerializeField] private Transform equippedItemsContainer;
    [SerializeField] private GameObject equippedItemPrefab;
    
    private Player _player;
    
    void OnEnable()
    {
        
        GameManager.OnGameStateSet += InitializePanel;
        GameState.OnTurnChanged += UpdateRollButton;
        GameState.OnStatsChanged += UpdateStats;
        GameState.OnPlayerItemsUpdated += UpdateItemsView;
    }

    void OnDisable()
    {
        GameManager.OnGameStateSet -= InitializePanel;
        GameState.OnTurnChanged -= UpdateRollButton;
        GameState.OnStatsChanged -= UpdateStats;
        GameState.OnPlayerItemsUpdated -= UpdateItemsView;
    }

    private void InitializePanel()
    {
        _player = GameManager.Instance.GameState.GetPlayer(PlayerProfile.Instance.Id);
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{_player.ProfilePicture}");
        playerNameText.text = _player.Name;
        UpdateItemsView(_player.Id);
        UpdateStats(_player.Id);
    }
    
    private void UpdateRollButton()
    {
        rollButton.interactable = GameManager.Instance.GameState.IsYourTurn();
    }

    private void UpdateStats(string playerId)
    {
        if (playerId == _player.Id)
        {
            coinsText.text = $"Coins: {_player.Coins}";
            battlePowerText.text = $"BP: {_player.BattlePower}";
            levelText.text = $"{_player.Level}";
        }
    }

    private void UpdateItemsView(string playerId)
    {
        if (playerId == _player.Id)
        {
            int bagItemsCount = _player.GetBagItems().Count;
            bagButton.interactable = bagItemsCount > 0;
            bagText.text = bagItemsCount > 0 ? $"Bag [{bagItemsCount}]" : "Bag";
            UpdateEquippedItems();
        }
    }
    
    private void UpdateEquippedItems()
    {
        equippedItemsContainer.Cast<Transform>().ToList().ForEach(child => Destroy(child.gameObject));
        
        List<EquipItem> equippedItems = GameManager.Instance.GameState.GetPlayer().GetEquippedItems();
        
        foreach (EquipItem item in equippedItems)
        {
            GameObject newEquippedItem = Instantiate(equippedItemPrefab, equippedItemsContainer);
            if (newEquippedItem.TryGetComponent(out EquippedItem equippedItem))
            {
               equippedItem.InitializeEquippedItem(item);
            }
        }
    }
    
    public void OnRollClicked()
    {
        rollButton.interactable = false;
        GameManager.Instance.RegisterRollDice();
    }
    
    public void OnBagClicked()
    {
        PopupManager.Instance.bagPopup.Display(BagState.Idle);
    }
    
}

