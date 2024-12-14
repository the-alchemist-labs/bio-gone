using TMPro;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text winnerText;

    public void Display(Player winner)
    {
        gameObject.SetActive(true);
        winnerText.text = $"{winner.Name} has won!";
    }

    public void FinishClicked()
    {
        Debug.Log("Game Over");
        // Move to Menu
    }
}
