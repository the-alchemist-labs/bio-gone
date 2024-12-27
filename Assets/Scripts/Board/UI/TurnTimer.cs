using TMPro;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    void OnEnable()
    {
        GameState.OnTimerUpdated += UpdateTimer;
    }

    void OnDisable()
    {
        GameState.OnTimerUpdated -= UpdateTimer;
    }

    private void UpdateTimer(int seconds)
    {
        timerText.text = seconds.ToString();
    }
}
