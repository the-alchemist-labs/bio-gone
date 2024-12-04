using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public CommandsQueue CommandsQueue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            gameObject.AddComponent<MainThreadDispatcher>();
            gameObject.AddComponent<PlayerProfile>();
            
            GameState = new GameState();
            CommandsQueue = new CommandsQueue();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
