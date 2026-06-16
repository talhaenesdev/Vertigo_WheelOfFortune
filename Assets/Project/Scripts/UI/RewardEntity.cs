using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class RewardEntity : MonoBehaviour
    {
        [SerializeField] private Image _rewardImage;
        [SerializeField] private TMP_Text _rewardText;

        internal void SetReward(Sprite sprite, string text)
        {
            _rewardImage.sprite = sprite;
            _rewardImage.SetNativeSize();
            _rewardText.text = text;
        }
    }

}
