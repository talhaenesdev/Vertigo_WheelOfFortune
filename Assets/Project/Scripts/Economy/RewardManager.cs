using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Economy
{
    internal class RewardManager : MonoBehaviour, IRewardService
    {

        Dictionary<RewardType, int> _rewardAmounts = new Dictionary<RewardType, int>();


        public void AddReward(RewardData reward) => _rewardAmounts[reward.RewardType] = _rewardAmounts.GetValueOrDefault(reward.RewardType) + reward.Amount;

        public int GetRewardAmount(RewardType rewardType)
        {
            return _rewardAmounts.GetValueOrDefault(rewardType, 0);
        }

        public void ResetReward() => _rewardAmounts.Clear();



        public void CollectReward()
        {
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

        public Dictionary<RewardType, int> GetRewardData()
        {
            RewardSaveData rewardSaveData = new RewardSaveData();
            return rewardSaveData.LoadRewards();
        }

    }
}