using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject _notEnoughCoinsPopup;
        [SerializeField] private GameObject _adPopup;
        [SerializeField] private Button _closeNotEnoughCoinsButton;
        [SerializeField] private Button _closeAdPopupButton;
        [SerializeField] private Button _whatchAdPopupButton;


        internal Action OnWatchAd;

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void SubscribeEvents()
        {
            _closeNotEnoughCoinsButton.onClick.AddListener(HideNotEnoughCoinsPopup);
            _closeAdPopupButton.onClick.AddListener(HideAdPopup);
            _whatchAdPopupButton.onClick.AddListener(HandleWatchAd);
        }
        private void UnSubscribeEvents()
        {
            _closeNotEnoughCoinsButton.onClick.RemoveListener(HideNotEnoughCoinsPopup);
            _closeAdPopupButton.onClick.RemoveListener(HideAdPopup);
            _whatchAdPopupButton.onClick.RemoveListener(HandleWatchAd);
        }
        internal void ShowNotEnoughCoinsPopup() => _notEnoughCoinsPopup.SetActive(true);


        internal void HideNotEnoughCoinsPopup() => _notEnoughCoinsPopup.SetActive(false);


        internal void ShowAdPopup() => _adPopup.gameObject.SetActive(true);


        private void HideAdPopup() => _adPopup.SetActive(false);


        private void HandleWatchAd()
        {
            OnWatchAd?.Invoke();
            HideAdPopup();
        }
    }
}
