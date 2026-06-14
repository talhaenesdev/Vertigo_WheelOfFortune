using System.Collections.Generic;
using UnityEngine;

public class WheelView : MonoBehaviour
{
    [SerializeField] List<RewardEntity> _rewardImages = new List<RewardEntity>();

    public void SetRewardView(int index, Sprite sprite, string text)
    {
        if (index >= 0 && index < _rewardImages.Count)
        {
            _rewardImages[index].SetReward(sprite, text);
        }
    }

}
