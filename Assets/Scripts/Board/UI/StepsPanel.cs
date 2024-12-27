using TMPro;
using UnityEngine;

public class StepsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;

    void OnEnable()
    {
        GameState.OnStepsChanged += UpdateStepsText;
    }

    void OnDisable()
    {
        GameState.OnStepsChanged -= UpdateStepsText;
    }

    void UpdateStepsText(int? steps)
    {
        playerNameText.text = steps.HasValue ? $"{steps}" : "";
    }
}