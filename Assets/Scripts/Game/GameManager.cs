using System.Collections.Generic;
using System.Threading.Tasks;
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
        Commander = new Commander();

        // move to SearchMatch & MatchFound matchmaking
        // change SearchMatch to http
        SocketIO.Instance.EmitEvent(SocketEvents.SearchMatch, new SearchMatchEvent(PlayerProfile.Instance.Id));
        SocketIO.Instance.RegisterEvent<MatchFoundEvent>(SocketEvents.MatchFound, GameManager.Instance.InitGame);

        // test
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            new RollDiceCommandPayload(PlayerProfile.Instance.Id, 3)));
    }

    async private void InitGame(MatchFoundEvent matchFoundEvent)
    {
        List<Player> players = await GetPlayers(matchFoundEvent.PlayerIds);
        GameState = new GameState(matchFoundEvent.RoomId, players);
    }

    async private Task<List<Player>> GetPlayers(string[] playerIds)
    {
        // get players profile
        return new List<Player>
        {
            new Player(playerIds[0], TileId.A1),
        };
    }
}