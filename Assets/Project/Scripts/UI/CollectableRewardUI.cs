using Assets.Project.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class CollectableRewardUI : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TMP_Text _rewardAmountText;

        internal RewardType RewardType { get; private set; }

        internal void Setup(RewardType rewardType, Sprite rewardSprite, int rewardAmount)
        {
            RewardType = rewardType;
            _rewardImage.sprite = rewardSprite;
            _rewardImage.preserveAspect = true;
            SetAmountText(rewardAmount);
        }

        internal void SetAmountText(int amount)
        {
            _rewardAmountText.text = amount.ToString();
        }
    }

}
