using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GamePrerequisite
{
    Socket,
    Player,
}
public class MainManager : MonoBehaviour
{
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

        if (_isSocketReady && _isPlayerReady)
        {
            SceneManager.LoadScene(SceneNames.Matchmaking);
        }
    }
}