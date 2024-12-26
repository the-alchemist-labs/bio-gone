using Unity.VisualScripting;
using UnityEngine;

public class BattlePopup : MonoBehaviour
{
    [SerializeField] private MonsterBattlePanel monsterBattlePanel;
    [SerializeField] private PlayerBattlePanel playerBattlePanel;
    [SerializeField] private BattleResultPanel resultPanel;
    [SerializeField] private VsPanel vsPanel;

    public void Display(Battle battle)
    {
        gameObject.SetActive(true);
        vsPanel.SetVsValues();

        monsterBattlePanel.Initialize(battle);
        playerBattlePanel.Initialize(battle);
    }

    public void TryToFlee()
    {
        playerBattlePanel.OnFleePhase();
        vsPanel.SetVsValues();
    }

    public void Interrupt()
    {
        playerBattlePanel.OnInterruptPhase();
        vsPanel.SetVsValues();
    }

    public void PlayerAction()
    {
        playerBattlePanel.OnPlayerActionPhase();
        vsPanel.SetVsValues();
    }

    public void Result()
    {
        BattleResult battleResult = GameManager.Instance.Battle.GetBattleResult();
        resultPanel.DisplayBattleResult(battleResult);

        if (!GameManager.Instance.Battle.IsInBattle()) return;
        if (ShouldTakeDamage(battleResult))
        {
            GameManager.Instance.RegisterLivesUpdate(-1);
        }

        if (battleResult == BattleResult.Win)
        {
            Monster monster = GameManager.Instance.Battle.Monster;
            GameManager.Instance.RegisterCoinsUpdate(monster.CoinsGain);
            GameManager.Instance.RegisterExpUpdate(monster.ExperienceGain);
        }
        
        resultPanel.DisplayBattleResult(battleResult);
    }

    public void ClosePopup()
    {
        resultPanel.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CloseClicked()
    {
        GameManager.Instance.RegisterToggleBattle(false);
        GameManager.Instance.TakeStep();
    }

    private bool ShouldTakeDamage(BattleResult result)
    {
        return GameManager.Instance.Battle.IsInBattle() &&
               (result == BattleResult.Lose || result == BattleResult.FailedFlee);
    }
}