using UnityEngine;

public class BattlePopup : MonoBehaviour
{
    [SerializeField] private MonsterBattlePanel monsterBattlePanel;
    [SerializeField] private PlayerBattlePanel playerBattlePanel;
    [SerializeField] private VsPanel vsPanel;
    
    public void Display(Battle battle)
    {
        gameObject.SetActive(true);
        
        monsterBattlePanel.Initialize(battle);
        playerBattlePanel.Initialize(battle);
        vsPanel.SetVs(battle.Player.BattlePower, battle.Monster.BattlePower);
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
    
    public void Result()
    {
        playerBattlePanel.OnResultPhase();
    }
    
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
    
    public void CloseClicked()
    {
        GameManager.Instance.RegisterToggleBattle(false);
        GameManager.Instance.TakeStep();
    }
}
