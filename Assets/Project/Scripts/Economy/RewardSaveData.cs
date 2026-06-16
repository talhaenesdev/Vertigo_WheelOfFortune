using Assets.Project.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Economy
{
    public class RewardSaveData
    {
        private const string SAVE_KEY = "RewardAmounts";

        public void SaveRewards(Dictionary<RewardType, int> rewardAmounts)
        {
            RewardDictionarySaveData saveData = new();

            foreach (var reward in rewardAmounts)
            {
                saveData.Rewards.Add(new RewardEntry
                {
                    RewardType = reward.Key,
                    Amount = reward.Value
                });
            }

            string json = JsonUtility.ToJson(saveData);

            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();
        }

        public Dictionary<RewardType, int> LoadRewards()
        {
            Dictionary<RewardType, int> rewardAmounts = new();

            if (!PlayerPrefs.HasKey(SAVE_KEY))
                return rewardAmounts;

            string json = PlayerPrefs.GetString(SAVE_KEY);

            RewardDictionarySaveData saveData =
                JsonUtility.FromJson<RewardDictionarySaveData>(json);

            if (saveData == null)
                return rewardAmounts;

            foreach (var reward in saveData.Rewards)
            {
                rewardAmounts[reward.RewardType] = reward.Amount;
            }

            return rewardAmounts;
        }
    }
    
    
    [Serializable]
    public class RewardEntry
    {
        public RewardType RewardType;
        public int Amount;
    }

    [Serializable]
    public class RewardDictionarySaveData
    {
        public List<RewardEntry> Rewards = new();
    }
}
