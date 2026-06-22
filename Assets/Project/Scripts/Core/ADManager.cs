using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Utilities;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.Core
{
    public class ADManager : MonoBehaviour, IADService
    {
        [SerializeField] 
        private GameObject _adPanel;
        [SerializeField] 
        private Button _collectWatchRewardButton;
        [SerializeField] 
        private TMP_Text _timerValue;
        [SerializeField] 
        private AdData _adData;

        public event Action OnCollectAdReward;

#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private void AutoAssignReferences()
        {
            if (!_adPanel)
                _adPanel = UIHierarchyHelper.FindGameObject(transform, "ui_panel_ad");

            if (!_collectWatchRewardButton)
                _collectWatchRewardButton = UIHierarchyHelper.FindComponent<Button>(transform, "ui_button_ad_complete");

            if (!_timerValue)
                _timerValue = UIHierarchyHelper.FindComponent<TMP_Text>(transform, "ui_text_timer_value");
        }
#endif
        public void ShowAd()
        {
            _adPanel.SetActive(true);
            WatchAd();

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
        }

        private void InteractableButton(bool interactable) => _collectWatchRewardButton.interactable = interactable;
    }
}
