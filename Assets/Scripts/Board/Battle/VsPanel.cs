using TMPro;
using UnityEngine;

public class VsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text playerValueText;
    [SerializeField] private TMP_Text monsterValueText;

    public void SetVs(int playerValue, int monsterValue)
    {
        // Add specific image for battle and flee
        playerValueText.text = playerValue.ToString();
        monsterValueText.text = monsterValue.ToString();
    }
}