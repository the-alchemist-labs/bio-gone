using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchFoundPanel : MonoBehaviour
{
    public static event Action OnMatchmakingStart;

    [SerializeField] private int countDownSeconds;
    [SerializeField] private Image playerImage;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image opponentImage;
    [SerializeField] private TMP_Text opponentName;
    [SerializeField] private TMP_Text countDownText;

    private int _countDown;
    private float _timer; // Tracks time for 1-second intervals

    public void Display(MatchFoundEvent matchFoundEvent)
    {
        gameObject.SetActive(true);
        
        _countDown = countDownSeconds;
        PlayerData player = matchFoundEvent.PlayersData.Find(p => p.Id == PlayerProfile.Instance.Id);
        PlayerData opponent = matchFoundEvent.PlayersData.Find(p => p.Id != PlayerProfile.Instance.Id);

        playerImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{player.ProfilePicture}");
        playerName.text = player.Name;

        opponentImage.sprite = Resources.Load<Sprite>($"Sprites/ProfilePics/{opponent.ProfilePicture}");
        opponentName.text = opponent.Name;
        countDownText.text = _countDown.ToString();
    }

    void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f && _countDown > 0)
        {
            _countDown--;
            _timer = 0f;
            countDownText.text = _countDown.ToString();

            if (_countDown <= 0)
            {
                SceneManager.LoadScene(SceneNames.Game); 
            }
        }
    }

}