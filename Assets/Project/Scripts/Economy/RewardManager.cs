using Assets.Project.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Economy
{
    public class RewardManager : MonoBehaviour, IRewardService
    {

        Dictionary<string, int> _rewardAmounts = new Dictionary<string, int>();


        public void AddReward(RewardData reward)
        {
            _rewardAmounts[reward.RewardItem.Id] = _rewardAmounts.GetValueOrDefault(reward.RewardItem.Id) + reward.Amount;
        }
        public int GetRewardAmount(string rewardId)
        {
            return _rewardAmounts.GetValueOrDefault(rewardId, 0);
        }

        public void ResetReward() => _rewardAmounts.Clear();

        public void CollectReward()
        {
            RewardSaveData rewardSaveData = new RewardSaveData();

            Dictionary<string, int> hasRewards = new();

            hasRewards = rewardSaveData.LoadRewards();
            
            rewardSaveData.SaveRewards(
                MergeRewards(
                    _rewardAmounts,
                    hasRewards)
                );

            ResetReward();
        }

        private Dictionary<string, int> MergeRewards(
            Dictionary<string, int> source,
            Dictionary<string, int> target)
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

        public Dictionary<string, int> GetRewardData()
        {
            RewardSaveData rewardSaveData = new RewardSaveData();
            return rewardSaveData.LoadRewards();
        }

    }
}