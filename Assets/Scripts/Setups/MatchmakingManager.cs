using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchmakingManager : MonoBehaviour
{
    void Start()
    {
        SocketIO.Instance.RegisterEvent<MatchFoundEvent>(SocketEvents.MatchFound, OnMatchFound);
        SocketIO.Instance.EmitEvent(SocketEvents.SearchMatch, new SearchMatchEvent(PlayerProfile.Instance.Id));
    }

    private void OnMatchFound(MatchFoundEvent matchFoundEvent)
    {
        List<Player> players = GetPlayers(matchFoundEvent.PlayerIds);
        
        MatchFoundResults.Instance.Players = players;
        MatchFoundResults.Instance.RoomId = matchFoundEvent.RoomId;
        
        SceneManager.LoadScene(SceneNames.Game);
    }
    
    private List<Player> GetPlayers(string[] playerIds)
    {
        // get players profile
        return new List<Player>
        {
            new Player(playerIds[0], "Sol"),
        };
    }
}