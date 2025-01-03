using System;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    [SerializeField] private MatchFoundPanel matchFoundPanel;
    [SerializeField] private int countDownSeconds;
    void Start()
    {
        SocketIO.Instance.RegisterEvent<MatchFoundEvent>(SocketEvents.MatchFound, OnMatchFound);
        if (!SocketIO.Instance.Socket.Connected)
        {
            Debug.Log("Socket disconnected.");
        }
        SocketIO.Instance.EmitEvent(SocketEvents.SearchMatch, new SearchMatchEvent(PlayerProfile.Instance.Id));
    }

    private void OnMatchFound(MatchFoundEvent matchFoundEvent)
    {
        MatchFoundResults.Instance = matchFoundEvent;
        matchFoundPanel.Display(matchFoundEvent, countDownSeconds);
    }
}