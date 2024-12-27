using UnityEngine;

public class BattlePopup : MonoBehaviour, IPopup
{
    [SerializeField] private MonsterBattlePanel monsterBattlePanel;
    [SerializeField] private PlayerBattlePanel playerBattlePanel;
    [SerializeField] private BattleResultPanel resultPanel;
    [SerializeField] private VsPanel vsPanel;

    void OnEnable()
    {
        Battle.OnBattlePowerModified += vsPanel.SetVsValues;
    }

    void OnDisable()
    {
        Battle.OnBattlePowerModified -= vsPanel.SetVsValues;
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
        Battle battle = GameManager.Instance.Battle;
        BattleResult battleResult = battle.GetBattleResult();
        
        resultPanel.DisplayBattleResult(battleResult);

        if (!battle.IsInBattle()) return;
        if (ShouldTakeDamage(battleResult)) battle.LostBattle();
        if (battleResult == BattleResult.Win) battle.WonBattle();
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