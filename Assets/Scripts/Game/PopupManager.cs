using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] public DicePopup dicePopup;
    [SerializeField] public ShopPopup shopPopup;

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