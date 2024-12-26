using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DicePopup : MonoBehaviour
{
    [SerializeField] private Image diceImage;
    [SerializeField] private TMP_Text diceText;

    public void Display(int diceValue)
    {
        gameObject.SetActive(true);

        diceText.text = "";
        
        StartCoroutine(RandomizeDice(diceValue));
    }
    
    private System.Collections.IEnumerator RandomizeDice(int diceValue)
    {
        Sprite[] diceSprites = Resources.LoadAll<Sprite>("Sprites/Game/Dice");

        for(int i = 0; i <= 8; i++)
        {
            diceImage.sprite = diceSprites[Random.Range(0, diceSprites.Length)];
            yield return new WaitForSeconds(0.1f);
        }
        

        diceImage.sprite = diceSprites[diceValue - 1];
        diceText.text = $"{diceValue} steps";

        Invoke(nameof(Go), 1f);
    }
    
    private void Go()
    {
        gameObject.SetActive(false);
        
        if (GameManager.Instance.GameState.IsYourTurn())
        {
            GameManager.Instance.TakeStep();
        }
    }
}
