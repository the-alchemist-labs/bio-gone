using System;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    [SerializeField] private MatchFoundPanel matchFoundPanel;
    void Start()
    {
        SocketIO.Instance.RegisterEvent<MatchFoundEvent>(SocketEvents.MatchFound, OnMatchFound);
        SocketIO.Instance.EmitEvent(SocketEvents.SearchMatch, new SearchMatchEvent(PlayerProfile.Instance.Id));
    }

    private void OnMatchFound(MatchFoundEvent matchFoundEvent)
    {
        MatchFoundResults.Instance = matchFoundEvent;
        matchFoundPanel.Display(matchFoundEvent);
    }
}