using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text playerValueText;
    [SerializeField] private TMP_Text monsterValueText;
    [SerializeField] private Image playerDiceImage;
    [SerializeField] private Image monsterDiceImage;

    public void SetVsValues()
    {
        bool isFleeing = GameManager.Instance.Battle.Phase == BattlePhase.Flee;
        
        playerDiceImage.gameObject.SetActive(isFleeing);
        monsterDiceImage.gameObject.SetActive(isFleeing);

        playerValueText.gameObject.SetActive(!isFleeing);
        monsterValueText.gameObject.SetActive(!isFleeing);
        
        if (isFleeing) SetFleeRollValues();
        else SetBattlePowerValues();
    }

    private void SetBattlePowerValues()
    {
        Battle battle = GameManager.Instance.Battle;
        playerValueText.text = battle.Player.BattlePower.ToString();
        monsterValueText.text = battle.Monster.BattlePower.ToString();
    }

    private void SetFleeRollValues()
    {
        Battle battle = GameManager.Instance.Battle;
        StartCoroutine(RandomizeDice(playerDiceImage, battle.Player.FleeRollValue.Value));
        StartCoroutine(RandomizeDice(monsterDiceImage, battle.Monster.FleeRollValue.Value));
        
        if (battle.IsInBattle())Invoke(nameof(Next), 2);
    }

    private System.Collections.IEnumerator RandomizeDice(Image diceImage, int diceValue)
    {
        SoundManager.Instance.PlaySound(SoundId.Dice);
        Sprite[] diceSprites = Resources.LoadAll<Sprite>("Sprites/Game/Dice");

        for (int i = 0; i <= 8; i++)
        {
            diceImage.sprite = diceSprites[UnityEngine.Random.Range(0, diceSprites.Length)];
            yield return new WaitForSeconds(0.1f);
        }

        diceImage.sprite = diceSprites[diceValue];
    }

    private void Next()
    {
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Result);
    }
}