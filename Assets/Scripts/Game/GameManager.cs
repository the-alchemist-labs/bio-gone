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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Commander = new Commander();
        GameState = new GameState(MatchFoundResults.Instance.RoomId, MatchFoundResults.Instance.Players);
        InitPlayersPosition();
        
        // test
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            JsonConvert.SerializeObject(new RollDiceCommandPayload(PlayerProfile.Instance.Id, 3))
        ));
    }
    
    private void InitPlayersPosition()
    {
        GameState.Players[0].MovePlayer(TileId.A1);
    }
}