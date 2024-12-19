using System.Linq;
using UnityEngine;

public class LivesContainer : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject heartPrefab;

    public void UpdateLives(int lives)
    {
        container.Cast<Transform>().ToList().ForEach(child => Destroy(child.gameObject));
        Enumerable.Range(0, lives).ToList().ForEach(_ => Instantiate(heartPrefab, container));
    }
}
