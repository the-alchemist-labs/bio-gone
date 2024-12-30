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
    [SerializeField] private GameObject waitingNotice;
    [SerializeField] private GameObject bagButton;

    private bool _youAreFighting;
    
    public void Initialize(Battle battle)
    {
        _youAreFighting = battle.Player.Id == PlayerProfile.Instance.Id;

        PlayerInBattle player = battle.Player;
        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{player.ProfilePicture}");
        nameText.text = player.Name;
        levelText.text = player.Level.ToString();
        battlePowerText.text = player.BattlePower.ToString();

        Invoke("SetUpButtons", 0.2f);
    }
    
    private void SetUpButtons()
    {
        fightButtons.SetActive(false);
        opponentButtons.SetActive(false);
        waitingNotice.SetActive(!_youAreFighting);
        encounterButtons.SetActive(_youAreFighting);
    }
    
    public void OnInterruptPhase()
    {
        encounterButtons.SetActive(false);
        fightButtons.SetActive(false);
        
        if (!_youAreFighting && !HasBagItems())
        {
            OnDontInterruptClicked();
            return;
        }

        opponentButtons.SetActive(!_youAreFighting);
        waitingNotice.SetActive(_youAreFighting);
    }

    public void OnPlayerActionPhase()
    {
        encounterButtons.SetActive(false);
        opponentButtons.SetActive(false);

        bagButton.SetActive(HasBagItems());
        fightButtons.SetActive(_youAreFighting);
        waitingNotice.SetActive(!_youAreFighting);
    }
    
    public void OnFleePhase()
    {
        encounterButtons.SetActive(false);
        fightButtons.SetActive(false);
        waitingNotice.SetActive(false);
    }
    
    // Clickes
    public void OnEngageClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.OpponentInterrupt);
    }
    
    public void OnFleeClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        FleeBattle fleeBattle = new FleeBattle(Random.Range(0, 7), Random.Range(0, 7));
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Flee, null, fleeBattle);
    }
    
    public void OnBagClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        PopupManager.Instance.bagPopup.Display(BagState.Battle);
    }

    public void OnInterruptClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        PopupManager.Instance.bagPopup.Display(BagState.OpponentBattle);
    }
    
    public void OnDontInterruptClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.PlayerAction);
    }
    
    public void OnAttackClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        GameManager.Instance.RegisterBattlePhaseUpdate(BattlePhase.Result);
    }


    private bool HasBagItems()
    {
        return GameManager.Instance.GameState.GetPlayer().GetBagItems().Count > 0;
    }
}