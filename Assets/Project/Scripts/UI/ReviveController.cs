using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    public class ReviveController : MonoBehaviour
    {
        [SerializeField] private Button _whatchAdReviveButton;
        [SerializeField] private Button _coinReviveButton;

        public Action OnClickAdReviveButton;
        public Action OnClickCoinReviveButton;
        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            DeInitialize();
        }

        private void Initialize()
        {
            _whatchAdReviveButton.onClick.AddListener(OnWatchAdRevive);
            _coinReviveButton.onClick.AddListener(OnCoinRevive);
        }

        private void DeInitialize()
        {
            _whatchAdReviveButton.onClick.RemoveListener(OnWatchAdRevive);
            _coinReviveButton.onClick.RemoveListener(OnCoinRevive);
        }

        private void OnWatchAdRevive()
        {
            Debug.Log("Watch Ad Revive Clicked");
        }

        private void OnCoinRevive()
        {
            Debug.Log("Coin Revive Clicked");
        }
    }
}
