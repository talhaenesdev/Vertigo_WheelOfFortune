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
        [SerializeField] private ADData _adData;
        [SerializeField] private TMP_Text _timerText;

        public Action OnAdWatched;

        public void ShowAd()
        {
            _adPanel.SetActive(true);
            // Simulate ad watching and reward collection
            WatchAd();
        }

        private void OnEnable()
        {
            _collectWatchRewardButton.interactable = false;
            _collectWatchRewardButton.onClick.AddListener(CollectAdReward);
        }

        private void OnDisable()
        {
            _collectWatchRewardButton.onClick.RemoveListener(CollectAdReward);
        }

        private void CollectAdReward()
        {
            _adPanel.SetActive(false);
        }

        private void WatchAd()
        {
            DOVirtual.Int(55, 0, 55f, value =>
            {
                _timerText.text = value.ToString();
            }).OnComplete(() =>
            {
                _collectWatchRewardButton.interactable = true;
                OnAdWatched?.Invoke();
            });
        }
    }
}
