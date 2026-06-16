using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class WheelUI : MonoBehaviour
    {
        [SerializeField] List<RewardEntity> _rewardImages = new List<RewardEntity>();

        [SerializeField] private Image _pointerImage;
        [SerializeField] private Image _wheelImage;
        [SerializeField] private TMP_Text _wheelTitleText;
        [SerializeField] private TMP_Text _wheelBonusText;

        internal void SetRewardView(int index, Sprite sprite, string text)
        {
            if (index >= 0 && index < _rewardImages.Count)
            {
                _rewardImages[index].SetReward(sprite, text);
            }
        }

        internal void SetWheelVisuals(Sprite pointerSprite, Sprite wheelSprite, Color textColor, string wheelType, string zoneType)
        {
            _pointerImage.sprite = pointerSprite;
            _wheelImage.sprite = wheelSprite;
            _wheelTitleText.color = textColor;
            _wheelTitleText.text = wheelType;
            _wheelTitleText.color = textColor;
            _wheelBonusText.text = zoneType;
            _wheelBonusText.color = textColor;
        }

    }

}
