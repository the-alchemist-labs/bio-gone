using UnityEngine;
using System.Collections.Generic;

public class RewardsLocationContainer : MonoBehaviour
{
    public Transform SpawnLocation => spawnLocation;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform playerCoins;
    [SerializeField] private Transform playerExp;
    [SerializeField] private Transform opponentCoins;
    [SerializeField] private Transform opponentExp;

    private Dictionary<(RewardType, RewardDestination), Transform> _rewardLocations;

    private void Awake()
    {
        _rewardLocations = new Dictionary<(RewardType, RewardDestination), Transform>
        {
            { (RewardType.Coin, RewardDestination.Player), playerCoins },
            { (RewardType.Coin, RewardDestination.Opponent), opponentCoins },
            { (RewardType.Exp, RewardDestination.Player), playerExp },
            { (RewardType.Exp, RewardDestination.Opponent), opponentExp }
        };
    }

    public Vector3 GetDestination(RewardType rewardType, RewardDestination destination)
    {
        if (_rewardLocations.TryGetValue((rewardType, destination), out Transform target))
        {
            return target.position;
        }

        throw new System.NotImplementedException($"No destination for {rewardType} and {destination}");
    }
}