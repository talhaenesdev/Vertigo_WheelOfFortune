using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    public class PopupManager : MonoBehaviour
    {
        [SerializeField] private GameObject _notEnoughCoinsPopup;
        [SerializeField] private GameObject _adPopup;
        [SerializeField] private Button _closeNotEnoughCoinsButton;
        [SerializeField] private Button _closeAdPopupButton;
        [SerializeField] private Button _whatchAdPopupButton;


        public Action OnWatchAd;

        private void OnEnable()
        {
            _closeNotEnoughCoinsButton.onClick.AddListener(HideNotEnoughCoinsPopup);
            _closeAdPopupButton.onClick.AddListener(HideAdPopup);
            _whatchAdPopupButton.onClick.AddListener(HandleWatchAd);
        }
        private void OnDisable()
        {
            _closeNotEnoughCoinsButton.onClick.RemoveListener(HideNotEnoughCoinsPopup);
            _closeAdPopupButton.onClick.RemoveListener(HideAdPopup);
            _whatchAdPopupButton.onClick.RemoveListener(HandleWatchAd);
        }

        public void ShowNotEnoughCoinsPopup()
        {
            _notEnoughCoinsPopup.SetActive(true);
        }

        public void HideNotEnoughCoinsPopup()
        {
            _notEnoughCoinsPopup.SetActive(false);
        }

        public void ShowAdPopup()
        {
            _adPopup.gameObject.SetActive(true);
        }

        private void HideAdPopup()
        {
            _adPopup.SetActive(false);
        }

        private void HandleWatchAd()
        {
            OnWatchAd?.Invoke();
        }
    }
}
