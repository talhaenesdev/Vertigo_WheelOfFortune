using UnityEngine;
namespace Assets.Project.Scripts.GamePlay
{
    public class RewardManager : MonoBehaviour
    {
        public int TotalReward { get; private set; }

        public void AddReward(int amount)
        {
            TotalReward += amount;
        }

        public void ResetReward()
        {
            TotalReward = 0;
        }
    }
}
