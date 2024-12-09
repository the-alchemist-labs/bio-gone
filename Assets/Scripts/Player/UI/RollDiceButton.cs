using UnityEngine;
using UnityEngine.UI;

public class RollDiceButton : MonoBehaviour
{
    [SerializeField] private Button rollButton;

    void OnEnable()
    {
        GameState.OnTurnChanged += UpdateUI;
    }

    void OnDisable()
    {
        GameState.OnTurnChanged -= UpdateUI;
    }
    private void UpdateUI(bool yourTurn)
    {
        rollButton.enabled = yourTurn;
    }

    public void OnRollClicked()
    {
        GameManager.Instance.RegisterRollDice();
    }
}