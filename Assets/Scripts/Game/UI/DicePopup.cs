using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DicePopup : MonoBehaviour
{
    [SerializeField] private Image diceImage;
    [SerializeField] private TMP_Text diceText;
    [SerializeField] private Button goButton;

    private bool _isYourTurn;
    public void Display(int diceValue)
    {
        gameObject.SetActive(true);
        
        _isYourTurn = GameManager.Instance.GameState.IsYourTurn();
        // TODO: play dice animation
        // land on dice value
        diceText.text = $"{diceValue} steps";
        
        goButton.gameObject.SetActive(_isYourTurn);
        
        if (!_isYourTurn)
        {
            Invoke("HidePopup", 3f);
        }
    }

    public void Go()
    {
        GameManager.Instance.TakeStep();
        HidePopup();
    }

    private void HidePopup()
    {
        gameObject.SetActive(false);

    }
}
