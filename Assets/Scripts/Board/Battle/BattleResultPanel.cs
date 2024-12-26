using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Image resultImage;
    
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TMP_Text loseText;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text coinsText;
    
    public void DisplayBattleResult(BattleResult result)
    {
        gameObject.SetActive(true);
        string playerName = GetPlayerName(PlayerProfile.Instance.Id);
        
        resultImage.sprite = Resources.Load<Sprite>($"Sprites/Battle/{result}");
        winPanel.SetActive(result == BattleResult.Win);
        losePanel.SetActive(result == BattleResult.Lose || result == BattleResult.FailedFlee);
        
        Invoke(nameof(EndBattle), 3);
        switch (result)
        {
            case BattleResult.Win:
                Monster monster = GameManager.Instance.Battle.Monster;
                resultText.text = $"{playerName} won!";
                expText.text = monster.ExperienceGain.ToString();
                coinsText.text = monster.CoinsGain.ToString();
                break;
            case BattleResult.Lose:
                resultText.text = $"{playerName} lost!";
                break;
            case BattleResult.Draw:
                resultText.text = $"{playerName} draw!";
                break;
            case BattleResult.Fled:
                resultText.text = $"{playerName} fled successfully!";
                break;
            case BattleResult.FailedFlee:
                resultText.text = $"{playerName} failed to flee.";
                break;
        }
    }
    
    private string GetPlayerName(string playerId)
    {
        bool isInBattle = GameManager.Instance.Battle.IsInBattle(playerId);
        return isInBattle ? "You" : GameManager.Instance.Battle.Player.Name;
    }

    private void EndBattle()
    {
        GameManager.Instance.RegisterToggleBattle(false);
    }
}
