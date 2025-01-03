using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GamePrerequisite
{
    Socket,
    Player,
}
public class MainManager : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    private static MainManager _instance;

    private bool _isSocketReady = false;
    private bool _isPlayerReady = false;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            
            gameObject.AddComponent<MainThreadDispatcher>();
            gameObject.AddComponent<SocketIO>();
            gameObject.AddComponent<PlayerProfile>();
            gameObject.AddComponent<ItemCatalog>();
            gameObject.AddComponent<MonsterCatalog>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;

        SocketIO.OnSocketConnected += PrepareGame;
        PlayerProfile.OnPlayerInit += PrepareGame;
        text.text = $"isSocketReady: {_isSocketReady} | isPlayerReady: {_isPlayerReady}\n";
    }

    void OnDestroy()
    {
        SocketIO.OnSocketConnected -= PrepareGame;
        PlayerProfile.OnPlayerInit -= PrepareGame;
    }

    
    void PrepareGame(GamePrerequisite prerequisite)
    {
        switch (prerequisite)
        {
            case GamePrerequisite.Socket:
                _isSocketReady = true;
                break;
            case GamePrerequisite.Player:
                _isPlayerReady = true;
                break;
        }
        
        text.text += $"isSocketReady: {_isSocketReady} | isPlayerReady: {_isPlayerReady}\n";

        if (_isSocketReady && _isPlayerReady)
        {
            SceneManager.LoadScene(SceneNames.MainMenu);
        }
    }
}