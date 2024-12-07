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
        MatchFoundResults.Instance = matchFoundEvent;
        SceneManager.LoadScene(SceneNames.Game);
    }
}