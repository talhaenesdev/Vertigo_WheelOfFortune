using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class CollectableRewardUI : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TMP_Text _rewardAmountText;

        internal string _rewardId { get; private set; }

        internal void Setup(string rewardId, Sprite rewardSprite, int rewardAmount)
        {
            _rewardId = rewardId;
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
