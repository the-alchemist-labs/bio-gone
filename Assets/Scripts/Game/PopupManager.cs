using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] public DicePopup dicePopup;
    [SerializeField] public ShopPopup shopPopup;
    [SerializeField] public BattlePopup battlePopup;
    [SerializeField] public BagPopup bagPopup;
    [SerializeField] public GameOverPopup gameOverPopup;
    [SerializeField] public ItemPopup itemPopup;
    
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
}