using Assets.Project.Scripts.Data;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.Core
{
    public class ADManager : MonoBehaviour
    {
        [SerializeField] private GameObject _adPanel;
        [SerializeField] private Button _collectWatchRewardButton;
        [SerializeField] private AdData _adData;
        [SerializeField] private TMP_Text _timerText;

        internal Action OnCollectAdReward;

        internal void ShowAd()
        {
            _adPanel.SetActive(true);
            // Simulate ad watching and reward collection
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
                _timerText.text = value.ToString();
            }).OnComplete(() =>
            {
                InteractableButton(true);
            });
        }

        private void InteractableButton(bool interactable) => _collectWatchRewardButton.interactable = interactable;
    }
}
