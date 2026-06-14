using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardEntity : MonoBehaviour
{
    [SerializeField] private Image _rewardImage;
    [SerializeField] private TMP_Text _rewardText;

    public void SetReward(Sprite sprite, string text)
    {
        _rewardImage.sprite = sprite;
        _rewardImage.preserveAspect = true;
        _rewardText.text = text;
    }
}
