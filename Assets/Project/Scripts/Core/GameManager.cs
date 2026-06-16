using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Economy;
using Assets.Project.Scripts.Enums;
using Assets.Project.Scripts.GamePlay;
using Assets.Project.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private RewardManager _rewardManager;
        [SerializeField] private ZoneManager _zoneManager;
        [SerializeField] private PopupManager _popupManager;
        [SerializeField] private CurrencyManager _currencyManager;
        [SerializeField] private ADManager _adManager;

        [SerializeField]
        private ReviveData _reviveData;
        
        private GameState _currentState;

        private void Start()
        {
            SubscribeEvents();
            InitializeGame();
        }
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
        private void SubscribeEvents()
        {
            _uiManager.OnSpinPressed += HandleSpin;
            _uiManager.OnCollectPressed += HandleCollect;
            _uiManager.OnRestartPressed += HandleRestart;
            _uiManager.OnWhatchAdReviveButton += HandleWhatchAdRevive;
            _uiManager.OnCoinReviveButton += HandleCoinRevive;
            _uiManager.OnOpenInventoryButton += OnOpenInventory;

            _popupManager.OnWatchAd += OnClickWatchAd;
            _adManager.OnCollectAdReward += OnAdWatched;
        }
        private void UnsubscribeEvents()
        {
            _uiManager.OnSpinPressed -= HandleSpin;
            _uiManager.OnCollectPressed -= HandleCollect;
            _uiManager.OnRestartPressed -= HandleRestart;
            _uiManager.OnWhatchAdReviveButton -= HandleWhatchAdRevive;
            _uiManager.OnCoinReviveButton -= HandleCoinRevive;
            _uiManager.OnOpenInventoryButton -= OnOpenInventory;

            _popupManager.OnWatchAd -= OnClickWatchAd;
            _adManager.OnCollectAdReward -= OnAdWatched;
        }

        private void InitializeGame()
        {
            UpdateCollectButton();
            SetupWheelConfig();
            SetWheel();
            _currentState = GameState.WaitingForInput;
            _uiManager.SetGameOverPanel(false);
            _uiManager.SetSpinButtonInteractable(true);
        }

        private void HandleRestart()
        {
            _zoneManager.ResetZone();
            _rewardManager.ResetReward();
            _uiManager.UpdateZone(_zoneManager.CurrentZone);
            _uiManager.SetGameOverPanel(false);
            _uiManager.ClearRewardArea();
            _uiManager.SetSpinButtonInteractable(true);
            InitializeGame();
        }

        private void HandleCollect()
        {
            if (_currentState != GameState.WaitingForInput)
                return;

            if (!_zoneManager.CanCollect())
                return;

            _currentState = GameState.GameOver;

            _rewardManager.CollectReward();
            HandleRestart();
            Debug.Log("[GameManager] - HandleCollect");
        }

        private void HandleSpin()
        {
            if (_currentState != GameState.WaitingForInput)
                return;

            _currentState = GameState.Spinning;

            _uiManager.SetCollectButtonInteractable(false);
            _uiManager.SetSpinButtonInteractable(false);

            SetupWheelConfig();

            _wheelController.Spin(OnSpinCompleted, _zoneManager.CurrentZoneIndex());
        }

        private void OnSpinCompleted(WheelSliceData result)
        {
            Debug.Log("[GameManager] - OnSpinCompleted");

            if (result.SliceType == SliceType.Bomb)
            {
                HandleBomb();
                return;
            }

            if (result.Reward != null)
                _rewardManager.AddReward(result.Reward);

            var rewardType = result.Reward.RewardType;

            _uiManager.AddRewardArea(
                rewardType,
                _uiManager.GetRewardIcon(rewardType),
                _rewardManager.GetRewardAmount(rewardType));

            _zoneManager.NextZone();

            _uiManager.UpdateZone(_zoneManager.CurrentZone);

            UpdateCollectButton();

            _uiManager.SetSpinButtonInteractable(true);

            SetWheel();

            _currentState = GameState.WaitingForInput;
        }

        private void HandleBomb()
        {
            Debug.Log("[GameManager] - HandleBomb");
            _currentState = GameState.Limbo;

            _rewardManager.ResetReward();

            _uiManager.SetGameOverPanel(true);
        }

        private void SetupWheelConfig() => _wheelController.SetConfig(_zoneManager.GetWheelConfig());

        private void SetWheel()
        {
            Debug.Log("[GameManager] - SetWheel");
            _uiManager.SetWheelVisual(
                _zoneManager.GetCurrentZoneType(),
                _zoneManager.GetWheelSliceRows());
        }

        private void UpdateCollectButton() => _uiManager.SetCollectButtonInteractable(_zoneManager.CanCollect());

        private void HandleCoinRevive()
        {
            Debug.Log("[GameManager] - HandleCoinRevive");
            if (_currencyManager.GetCurrentCoins() >= _reviveData.CoinReviveCost)
            {
                _currencyManager.SpendCoins(_reviveData.CoinReviveCost);
                InitializeGame();
                _uiManager.UpdateCurrencyText(_currencyManager.GetCurrentCoins());
            }
            else
            {
                _popupManager.ShowNotEnoughCoinsPopup();
            }
        }

        private void OnOpenInventory()
        {
            Debug.Log("[GameManager] - OnOpenInventory");
            Dictionary<RewardType, int> inventory = new Dictionary<RewardType, int>();

            inventory = _rewardManager.GetRewardData();

            List<InventoryItemVO> inventoryItems = new List<InventoryItemVO>();

            foreach (var item in inventory)
            {
                inventoryItems.Add(new InventoryItemVO
                {
                    RewardType = item.Key,
                    RewardSprite = _uiManager.GetRewardIcon(item.Key),
                    RewardAmount = item.Value
                });
            }

            _uiManager.OpenInventoryPanel(inventoryItems);
        }


        private void HandleWhatchAdRevive() => _popupManager.ShowAdPopup();

        private void OnClickWatchAd() => _adManager.ShowAd();

        private void OnAdWatched() => InitializeGame();
    }
}