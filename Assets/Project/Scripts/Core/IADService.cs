using System;

namespace Assets.Project.Scripts.Core
{
    internal interface IADService
    {
        event Action OnCollectAdReward;
        void ShowAd();
    }
}
