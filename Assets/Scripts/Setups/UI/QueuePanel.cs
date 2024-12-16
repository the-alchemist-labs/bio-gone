using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QueuePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text queueText;

    private int _queueStartTimestamp;

    void Start()
    {
        _queueStartTimestamp = Mathf.FloorToInt(Time.time);
    }
    
    void FixedUpdate()
    {
        queueText.text = $"Queueing\n{GetTimeDifference()}";
    }

    private string GetTimeDifference()
    {
        int elapsedTime = Mathf.FloorToInt(Time.time) - _queueStartTimestamp;

        int minutes = elapsedTime / 60;
        int seconds = elapsedTime % 60;

        return $"{minutes:D2}:{seconds:D2}";
    }

    public async void Back()
    {
        await MatchmakingApi.RemovePlayerFromLobby();
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}