using Unity.VisualScripting;
using UnityEngine;

public class BattlePopup : MonoBehaviour
{
    [SerializeField] private MonsterBattlePanel monsterBattlePanel;
    [SerializeField] private PlayerBattlePanel playerBattlePanel;
    [SerializeField] private BattleResultPanel resultPanel;
    [SerializeField] private VsPanel vsPanel;

    void OnEnable()
    {
        VsPanel.OnVsValuesUpdated += vsPanel.SetVsValues;
    }

    void OnDisable()
    {
        VsPanel.OnVsValuesUpdated -= vsPanel.SetVsValues;
    }

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
    }

    public void Interrupt()
    {
        playerBattlePanel.OnInterruptPhase();
    }

    public void PlayerAction()
    {
        playerBattlePanel.OnPlayerActionPhase();
    }

    public void Result(bool? hasEscaped)
    {
        BattleResult battleResult = GameManager.Instance.Battle.GetBattleResult(hasEscaped);
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