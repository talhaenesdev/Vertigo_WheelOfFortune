using Assets.Project.Scripts.Utilities;
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

#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private void AutoAssignReferences()
        {
            if (!_notEnoughCoinsPopup)
                _notEnoughCoinsPopup = UIHierarchyHelper.FindGameObject(
                    transform, "ui_popup_not_enough_money_parent");

            if (!_adPopup)
                _adPopup = UIHierarchyHelper.FindGameObject(
                    transform, "ui_popup_ad_parent");

            if (!_closeNotEnoughCoinsButton)
                _closeNotEnoughCoinsButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_popup_not_enough_money/ui_button_continue");

            if (!_closeAdPopupButton)
                _closeAdPopupButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_popup_ad_parent/ui_popup_ad/ui_button_close");

            if (!_whatchAdPopupButton) 
                _whatchAdPopupButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_popup_ad_parent/ui_popup_ad/ui_button_watch");   
        }
#endif

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
