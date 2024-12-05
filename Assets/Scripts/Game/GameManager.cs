using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameState GameState { get; set; }
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
        Commander = new Commander();

        // move to matchmaking
        // change SearchMatch to http
        SocketIO.Instance.EmitEvent(SocketEvents.SearchMatch, new SearchMatchEvent(PlayerProfile.Instance.Id));

        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            new RollDiceCommandPayload(PlayerProfile.Instance.Id, 3)));
    }

    // access from matchmaking scene after the socket response from the server with the roomId and playerIds
    async private void InitGame(string roomId, string[] playerIds)
    {
        List<Player> players = await GetPlayers(playerIds);
        GameState = new GameState(roomId, players);
    }
    
    async private Task<List<Player>> GetPlayers(string[] playerIds)
    {
         return new List<Player>
        {
            new Player(PlayerProfile.Instance.Id, TileId.A1),
            new Player("asd", TileId.B1),
        };
    }
}