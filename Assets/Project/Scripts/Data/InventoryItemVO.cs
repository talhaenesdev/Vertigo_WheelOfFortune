using Assets.Project.Scripts.Enums;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    public class InventoryItemVO
    {
        public RewardType RewardType { get; set; }
        public Sprite RewardSprite { get; set; }
        public int RewardAmount { get; set; }
    }
}
