using Assets.Project.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableRewardUI : MonoBehaviour
{
    [SerializeField] private Image rewardImage;
    [SerializeField] private TMP_Text rewardAmountText;

    public RewardType RewardType { get; private set; }

    public void Setup(RewardType rewardType, Sprite rewardSprite, int rewardAmount)
    {
        RewardType = rewardType;
        rewardImage.sprite = rewardSprite;
        rewardImage.preserveAspect = true;
        SetAmountText(rewardAmount);
    }

    public void SetAmountText(int amount)
    {
        rewardAmountText.text = amount.ToString();
    }
}
