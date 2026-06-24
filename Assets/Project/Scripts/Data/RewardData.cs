using System;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [Serializable]
    public class RewardData
    {
        public ItemDefinition RewardItem;
        [Min(1)]
        public int Amount;
    }
}