using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.UI;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.Core
{
    public class ADManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _adPanel;
        [SerializeField] 
        private Button _collectWatchRewardButton;
        [SerializeField] 
        private TMP_Text _timerValue;
        [SerializeField] 
        private AdData _adData;

        internal Action OnCollectAdReward;
#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private void AutoAssignReferences()
        {
            if (_adPanel == null)
                _adPanel = UIHierarchyHelper.FindGameObject(
                    transform, "ui_panel_ad");

            if (_collectWatchRewardButton == null)
                _collectWatchRewardButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_button_ad_complete");

            if (_timerValue == null)
                _timerValue = UIHierarchyHelper.FindComponent<TMP_Text>(
                    transform, "ui_text_timer_value");
        }
#endif
        internal void ShowAd()
        {
            _adPanel.SetActive(true);
            WatchAd();

            Debug.Log("[ADManager] - ShowAd");
        }

        private void OnEnable()
        {
            InteractableButton(false);
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void SubscribeEvents() => _collectWatchRewardButton.onClick.AddListener(CollectAdReward);

        private void UnSubscribeEvents() => _collectWatchRewardButton.onClick.RemoveListener(CollectAdReward);


        private void CollectAdReward()
        {
            _adPanel.SetActive(false);
            OnCollectAdReward?.Invoke();
            Debug.Log("[ADManager] - CollectAdReward");
        }

        private void WatchAd()
        {
            DOVirtual.Int(_adData.AdWaitTimeSeconds, 0, _adData.AdWaitTimeSeconds, value =>
            {
                _timerValue.text = value.ToString();
            }).OnComplete(() =>
            {
                InteractableButton(true);
            });
            Debug.Log("[ADManager] - WatchAd");
        }

        private void InteractableButton(bool interactable) => _collectWatchRewardButton.interactable = interactable;
    }
}
