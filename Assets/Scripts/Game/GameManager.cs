using System;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<bool> OnTurnChanged;
 
    public static event Action OnGameStateSet;
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    private Commander Commander { get; set; }
    
    private int TurnsLeft { get; set; }
    
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
        TurnsLeft = 0;
        Commander = new Commander();
        GameState = new GameState(MatchFoundResults.Instance);
        
        // show animation for who is starting

        OnGameStateSet?.Invoke();
        OnTurnChanged?.Invoke(IsYourTurn());
    }
    
    private bool IsYourTurn()
    {
        return GameState.Players[GameState.PlayerTurn].PlayerId == PlayerProfile.Instance.Id;
    }

    public void RollDice()
    {
        int rollValue = UnityEngine.Random.Range(Consts.MinRollValue, Consts.MaxRollValue);
        Commander.PostCommand(new CommandEvent(
            GameState.RoomId,
            Command.RollDice,
            JsonConvert.SerializeObject(new RollDiceCommandPayload(PlayerProfile.Instance.Id, rollValue))
        ));
    }

}