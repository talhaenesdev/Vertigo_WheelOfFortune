using Assets.Project.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "RewardIcon",menuName = "Game/RewardIcon Config")]
    internal class RewardIconDatabase : ScriptableObject
    {
        [SerializeField] private List<RewardIconData> items;

        private Dictionary<RewardType, Sprite> cache;

        private void OnEnable()
        {
            cache = new Dictionary<RewardType, Sprite>();

            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                if (item.icon == null)
                    continue;

                cache[item.rewardType] = item.icon;
            }
        }

        internal Sprite GetIcon(RewardType type)
        {
            return cache.TryGetValue(type, out var icon) ? icon : null;
        }
    }

    [Serializable]
    internal class RewardIconData
    {
        public RewardType rewardType;
        public Sprite icon;
    }

}
