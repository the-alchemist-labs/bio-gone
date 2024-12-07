using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    private Commander Commander { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            gameObject.AddComponent<MainThreadDispatcher>();
            gameObject.AddComponent<PlayerProfile>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(WaitForSocketConnection());
    }

    IEnumerator WaitForSocketConnection()
    {
        while (SocketIO.Instance == null)
        {
            Debug.Log("Waiting for socket to connect...");
            yield return new WaitForSeconds(0.1f); // Wait a short time before checking again
        }

        Debug.Log("Socket connected. Proceeding with the rest of the logic.");

        Commander = new Commander();
        Debug.Log("SearchMatch");
        Debug.Log("waste time");
        SocketIO.Instance.EmitEvent(SocketEvents.SearchMatch, new SearchMatchEvent(PlayerProfile.Instance.Id));
        SocketIO.Instance.RegisterEvent<MatchFoundEvent>(SocketEvents.MatchFound, GameManager.Instance.InitGame);
    }
    
    private void InitGame(MatchFoundEvent matchFoundEvent)
    {
        List<Player> players = GetPlayers(matchFoundEvent.PlayerIds);
        GameState = new GameState(matchFoundEvent.RoomId, players);
        InitPlayersPosition();
        
        // test
        Debug.Log("CommandEvent");
        Debug.Log("waste time");
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            JsonConvert.SerializeObject(new RollDiceCommandPayload(PlayerProfile.Instance.Id, 3))
            ));
    }

    private List<Player> GetPlayers(string[] playerIds)
    {
        // get players profile
        return new List<Player>
        {
            new Player(playerIds[0], "Sol"),
        };
    }

    private void InitPlayersPosition()
    {
        GameState.Players[0].MovePlayer(TileId.A1);
    }
}