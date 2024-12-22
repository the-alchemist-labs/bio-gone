using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum RewardType
{
    Coin,
    Exp,
}

public enum RewardDestination
{
    Player,
    Opponent,
}

public class RewardAnimationPool : MonoBehaviour
{
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private RewardsLocationContainer locations;
    [SerializeField] private float rewardingSpeed;

    private ObjectPool<GameObject> _pool;
    private Dictionary<RewardType, Sprite> _rewardSprites;


    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(rewardPrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: 15,
            maxSize: 25
        );

        _rewardSprites = new Dictionary<RewardType, Sprite>
        {
            { RewardType.Coin, Resources.Load<Sprite>("Sprites/Game/Coin") },
            { RewardType.Exp, Resources.Load<Sprite>("Sprites/Game/Exp") }
        };
    }

    public void DisplayRewardToken(RewardType rewardType, RewardDestination destType)
    {
        GameObject obj = _pool.Get();
        obj.transform.SetParent(locations.SpawnLocation, false);
        obj.transform.position = locations.SpawnLocation.position;

        RewardToken prefabScript = obj.GetComponent<RewardToken>();
        Sprite sprite = _rewardSprites[rewardType];
        Vector3 destination = locations.GetDestination(rewardType, destType);
        
        prefabScript.SpawnRewardToken (sprite, destination, rewardingSpeed, () => _pool.Release(obj));
    }

}