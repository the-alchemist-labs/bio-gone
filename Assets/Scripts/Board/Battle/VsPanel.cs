using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VsPanel : MonoBehaviour
{
    public static event Action OnVsValuesUpdated;
    
    [SerializeField] private TMP_Text playerValueText;
    [SerializeField] private TMP_Text monsterValueText;
    [SerializeField] private Image VsImage;

    public void SetVsValues()
    {
        Battle battle = GameManager.Instance.Battle;
        string vsType = battle.Phase == BattlePhase.Flee ? "Flee" : "Battle";
        
        // VsImage.sprite = Resources.Load<Sprite>($"Sprites/Battle/{vsType}");
        playerValueText.text = battle.Player.BattlePower.ToString();
        monsterValueText.text = battle.Monster.BattlePower.ToString();
    }

    
    public static void UpdateVsValues()
    {
        OnVsValuesUpdated?.Invoke();
    }
}