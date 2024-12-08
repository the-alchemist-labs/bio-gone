using UnityEngine;
using UnityEngine.UI;

public class RollDiceButton : MonoBehaviour
{
    [SerializeField] private Button rollButton;

    void OnEnable()
    {
        GameManager.OnTurnChanged += UpdateUI;
    }

    void OnDisable()
    {
        GameManager.OnTurnChanged -= UpdateUI;
    }
    private void UpdateUI(bool yourTurn)
    {
        rollButton.enabled = yourTurn;
    }

    public void OnRollClicked()
    {
        GameManager.Instance.RollDice();
    }
}