using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour, IPopup
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    public void Display(Player winner)
    {
        bool hasWon = winner.Id == PlayerProfile.Instance.Id;
        gameObject.SetActive(true);
        titleText.text = hasWon ? "You won!" : "You lost!";
        image.sprite = Resources.Load<Sprite>($"Sprites/Game/{(hasWon ? "Golden Apple" : "Worm")}");
        text.text = hasWon ? "Kept the doctor away!" : "Time for more immoral tests";

        SoundId sound = hasWon ? SoundId.WinGame : SoundId.LoseBattle;
        SoundManager.Instance.PlaySound(sound);
    }

    public void FinishClicked()
    {
        SoundManager.Instance.PlaySound(SoundId.Click);
        ClosePopup();
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
    
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
