
using System;

namespace Assets.Project.Scripts.UI
{
    internal interface IPopupService
    {
        event Action OnWatchAd;
        void ShowNotEnoughCoinsPopup();
        void HideNotEnoughCoinsPopup();
        void ShowAdPopup();
        void HideAdPopup();
    }
}
