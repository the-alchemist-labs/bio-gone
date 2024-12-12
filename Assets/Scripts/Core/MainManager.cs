using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance;

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

        SocketIO.OnSocketConnected += StartGame;
    }

    void OnDestroy()
    {
        SocketIO.OnSocketConnected -= StartGame;
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneNames.Matchmaking);
    }
}