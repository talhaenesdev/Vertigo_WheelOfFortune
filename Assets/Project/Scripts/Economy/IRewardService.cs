using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Project.Scripts.Economy
{
    public interface IRewardService
    {
        void AddReward(RewardData reward);
        int GetRewardAmount(string rewardId);
        void ResetReward();
        void CollectReward();
        Dictionary<string, int> GetRewardData();

    }
}
