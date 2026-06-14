using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] List<RewardEntity> _rewardImages = new List<RewardEntity>();

        [SerializeField] private Image pointerImage;
        [SerializeField] private Image wheelImage;
        [SerializeField] private TMP_Text wheelTitleText;
        [SerializeField] private TMP_Text wheelBonusText;

        public void SetRewardView(int index, Sprite sprite, string text)
        {
            if (index >= 0 && index < _rewardImages.Count)
            {
                _rewardImages[index].SetReward(sprite, text);
            }
        }

        public void SetPointerImage(Sprite pointerSprite, Sprite wheelSprite, Color textColor)
        {
            pointerImage.sprite = pointerSprite;
            wheelImage.sprite = wheelSprite;
            wheelTitleText.color = textColor;
            wheelBonusText.color = textColor;
        }

    }

}
