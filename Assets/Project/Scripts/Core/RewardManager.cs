using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    public class RewardManager : MonoBehaviour
    {

        Dictionary<RewardType, int> rewardAmounts = new Dictionary<RewardType, int>();

        [SerializeField] private RewardIconDatabase rewardIconDatabase;

        public void AddReward(RewardData reward)
        {
            rewardAmounts[reward.RewardType] =
                rewardAmounts.GetValueOrDefault(reward.RewardType) + reward.Amount;
        }

        public int GetRewardAmount(RewardType rewardType)
        {
            return rewardAmounts.GetValueOrDefault(rewardType, 0);
        }

        public void ResetReward()
        {
            rewardAmounts.Clear();
        }

        public Sprite GetRewardIcon(RewardType rewardType)
        {
            return rewardIconDatabase.GetIcon(rewardType);
        }
    }
}