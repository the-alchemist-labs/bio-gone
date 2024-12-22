using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardToken : MonoBehaviour
{
    [SerializeField] Image image;

    public void SpawnRewardToken (Sprite sprite, Vector3 destination, float speed, Action onComplete)
    {
        image.sprite = sprite;
        StartCoroutine(MoveToPosition(destination, speed, onComplete));
    }
    
    private IEnumerator MoveToPosition(Vector3 targetPosition, float speed, Action onComplete)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float step = speed * 100 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            yield return null;
        }

        transform.position = targetPosition;
        onComplete();
    }
}