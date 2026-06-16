using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Economy
{
    internal class RewardManager : MonoBehaviour
    {

        Dictionary<RewardType, int> _rewardAmounts = new Dictionary<RewardType, int>();


        internal void AddReward(RewardData reward) => _rewardAmounts[reward.RewardType] = _rewardAmounts.GetValueOrDefault(reward.RewardType) + reward.Amount;

        internal int GetRewardAmount(RewardType rewardType)
        {
            return _rewardAmounts.GetValueOrDefault(rewardType, 0);
        }

        internal void ResetReward() => _rewardAmounts.Clear();



        internal void CollectReward()
        {
            Debug.Log("[RewardManager] - CollectReward");
            RewardSaveData rewardSaveData = new RewardSaveData();

            Dictionary<RewardType, int> hasRewards = new();

            hasRewards = rewardSaveData.LoadRewards();

            
            rewardSaveData.SaveRewards(
                MergeRewards(
                    _rewardAmounts,
                    hasRewards)
                );

            ResetReward();
        }

        private Dictionary<RewardType, int> MergeRewards(
            Dictionary<RewardType, int> source,
            Dictionary<RewardType, int> target)
        {
            foreach (var reward in source)
            {
                if (target.ContainsKey(reward.Key))
                {
                    target[reward.Key] += reward.Value;
                }
                else
                {
                    target.Add(reward.Key, reward.Value);
                }
            }

            return target;
        }

        internal Dictionary<RewardType, int> GetRewardData()
        {
            Debug.Log("[RewardManager] - GetRewardData");
            RewardSaveData rewardSaveData = new RewardSaveData();
            return rewardSaveData.LoadRewards();
        }

    }
}