using UnityEngine;
using UnityEngine.SceneManagement;

public class FindMatchButton: MonoBehaviour
{
    public void FindMatchClicked()
    {
        SceneManager.LoadScene(SceneNames.Matchmaking);
    }
}