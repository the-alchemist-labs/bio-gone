using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattlePanel : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text battlePowerText;

    [SerializeField] private GameObject encounterButtons;
    [SerializeField] private GameObject fightButtons;
    [SerializeField] private GameObject opponentButtons;
    [SerializeField] private GameObject fleeButtons;
    [SerializeField] private GameObject endButtons;

    private bool _youAreFighting;

    public void Initialize(Battle battle)
    {
        _youAreFighting = battle.Player.Id == PlayerProfile.Instance.Id;

        PlayerInBattle player = battle.Player;
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{player.ProfilePicture}");
        nameText.text = player.Name;
        levelText.text = player.Level.ToString();
        battlePowerText.text = player.BattlePower.ToString();

        fightButtons.SetActive(false);
        opponentButtons.SetActive(false);
        fleeButtons.SetActive(false);
        endButtons.SetActive(false);
        encounterButtons.SetActive(_youAreFighting);
    }
    
    // Updates

    public void OnInterruptPhase()
    {
        encounterButtons.SetActive(false);
        fightButtons.SetActive(false);
        opponentButtons.SetActive(true);
    }

    public void OnPlayerActionPhase()
    {
        encounterButtons.SetActive(false);
        opponentButtons.SetActive(false);
        fightButtons.SetActive(true);
    }
    
    public void OnResultPhase()
    {
        fightButtons.SetActive(false);
        endButtons.SetActive(true);
    }
    
    public void OnFleePhase()
    {
        encounterButtons.SetActive(false);
        fightButtons.SetActive(false);
        fleeButtons.SetActive(_youAreFighting);
    }
    
    // Clickes
    public void OnEngageClicked()
    {
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.OpponentInterrupt);
    }
    
    public void OnFleeClicked()
    {
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Flee);
    }
    
    public void OnBagClicked()
    {
        PopupManager.Instance.bagPopup.Display(BagState.Battle);
    }

    public void OnInterruptClicked()
    {
        PopupManager.Instance.bagPopup.Display(BagState.OpponentBattle);
    }
    
    public void OnDontInterruptClicked()
    {
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.PlayerAction);
    }
    
    public void OnAttackClicked()
    {
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Result);
    }
    
    public void OnEndClicked()
    {
        PopupManager.Instance.battlePopup.CloseClicked();
    }
}