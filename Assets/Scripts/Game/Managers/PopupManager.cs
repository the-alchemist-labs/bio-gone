using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IPopup
{
    void ClosePopup();
}

public class PopupManager : MonoBehaviour
{
    [SerializeField] public DicePopup dicePopup;
    [SerializeField] public ShopPopup shopPopup;
    [SerializeField] public BattlePopup battlePopup;
    [SerializeField] public BagPopup bagPopup;
    [SerializeField] public GameOverPopup gameOverPopup;
    [SerializeField] public ItemPopup itemPopup;

    private List<IPopup> _popups = new List<IPopup>();
    
    public static PopupManager Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        _popups = GetComponentsInChildren<IPopup>(true).ToList();
        SoundManager.Instance.PlayBGM(BackgroundMusicId.Main);
    }
    
    public void CloseAll()
    {
        _popups.ForEach(p => p.ClosePopup());
    }
}