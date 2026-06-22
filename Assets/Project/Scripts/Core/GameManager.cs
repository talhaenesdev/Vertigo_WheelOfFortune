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
        private IUIServices _uIServices;
        private IWheelService _wheelService;
        private IRewardService _rewardService;
        private IZoneService _zoneService;
        private IPopupService _popupService;
        private ICurrencyService _currencyService;
        private IADService _aDService;

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

        internal void Inject(IRewardService reward, 
            IZoneService zone, 
            IUIServices uI, 
            IWheelService wheel, 
            IPopupService popup, ICurrencyService currencyService, IADService aDService)
        {
            _rewardService = reward;
            _zoneService = zone;
            _uIServices = uI;
            _wheelService = wheel;
            _popupService = popup;
            _popupService = popup;
            _currencyService = currencyService;
            _aDService = aDService;
        }

        private void SubscribeEvents()
        {
            _uIServices.OnSpinPressed += HandleSpin;
            _uIServices.OnCollectPressed += HandleCollect;
            _uIServices.OnRestartPressed += HandleRestart;
            _uIServices.OnWhatchAdReviveButton += HandleWhatchAdRevive;
            _uIServices.OnCoinReviveButton += HandleCoinRevive;
            _uIServices.OnOpenInventoryButton += OnOpenInventory;

            _popupService.OnWatchAd += OnClickWatchAd;
            _aDService.OnCollectAdReward += OnAdWatched;
        }
        private void UnsubscribeEvents()
        {
            _uIServices.OnSpinPressed -= HandleSpin;
            _uIServices.OnCollectPressed -= HandleCollect;
            _uIServices.OnRestartPressed -= HandleRestart;
            _uIServices.OnWhatchAdReviveButton -= HandleWhatchAdRevive;
            _uIServices.OnCoinReviveButton -= HandleCoinRevive;
            _uIServices.OnOpenInventoryButton -= OnOpenInventory;

            _popupService.OnWatchAd -= OnClickWatchAd;
            _aDService.OnCollectAdReward -= OnAdWatched;
        }

        private void InitializeGame()
        {
            UpdateCollectButton();
            SetupWheelConfig();
            SetWheel();
            _currentState = GameState.WaitingForInput;
            _uIServices.SetGameOverPanel(false);
            _uIServices.SetSpinButtonInteractable(true);
            _uIServices.UpdateCurrencyText(_currencyService.GetCurrentCoins());
        }

        private void HandleRestart()
        {
            _zoneService.ResetZone();
            _rewardService.ResetReward();
            _uIServices.UpdateZone(_zoneService.CurrentZone);
            _uIServices.SetGameOverPanel(false);
            _uIServices.ClearRewardArea();
            _uIServices.SetSpinButtonInteractable(true);
            InitializeGame();
        }

        private void HandleCollect()
        {
            if (_currentState != GameState.WaitingForInput)
                return;

            if (!_zoneService.CanCollect())
                return;

            _currentState = GameState.GameOver;

            _rewardService.CollectReward();
            HandleRestart();
        }

        private void HandleSpin()
        {
            if (_currentState != GameState.WaitingForInput)
                return;

            _currentState = GameState.Spinning;

            _uIServices.SetCollectButtonInteractable(false);
            _uIServices.SetSpinButtonInteractable(false);

            SetupWheelConfig();

            _wheelService.Spin(OnSpinCompleted, _zoneService.CurrentZoneIndex());
        }

        private void OnSpinCompleted(WheelSliceData result)
        {

            if (result.SliceType == SliceType.Bomb)
            {
                HandleBomb();
                return;
            }

            if (result.Reward != null)
            {
                _rewardService.AddReward(result.Reward);

                var rewardType = result.Reward.RewardType;

                _uIServices.AddRewardArea(
                    rewardType,
                    _uIServices.GetRewardIcon(rewardType),
                    _rewardService.GetRewardAmount(rewardType));
            }

            _zoneService.NextZone();

            _uIServices.UpdateZone(_zoneService.CurrentZone);

            UpdateCollectButton();

            _uIServices.SetSpinButtonInteractable(true);

            SetWheel();

            _currentState = GameState.WaitingForInput;
        }

        private void HandleBomb()
        {
            _currentState = GameState.Limbo;


            _uIServices.SetGameOverPanel(true);
        }

        private void SetupWheelConfig() => _wheelService.SetConfig(_zoneService.GetWheelConfig());

        private void SetWheel()
        {
            _uIServices.SetWheelVisual(
                _zoneService.GetCurrentZoneType(),
                _zoneService.GetWheelSliceRows());
        }

        private void UpdateCollectButton() => _uIServices.SetCollectButtonInteractable(_zoneService.CanCollect());

        private void HandleCoinRevive()
        {
            if (_currencyService.GetCurrentCoins() >= _reviveData.CoinReviveCost)
            {
                _currencyService.SpendCoins(_reviveData.CoinReviveCost);
                InitializeGame();
                _uIServices.UpdateCurrencyText(_currencyService.GetCurrentCoins());
            }
            else
            {
                _popupService.ShowNotEnoughCoinsPopup();
            }
        }

        private void OnOpenInventory()
        {
            var inventory = _rewardService.GetRewardData();

            List<InventoryItemVO> inventoryItems = new List<InventoryItemVO>();

            foreach (var item in inventory)
            {
                inventoryItems.Add(new InventoryItemVO
                {
                    RewardType = item.Key,
                    RewardSprite = _uIServices.GetRewardIcon(item.Key),
                    RewardAmount = item.Value
                });
            }

            _uIServices.OpenInventoryPanel(inventoryItems);
        }


        private void HandleWhatchAdRevive() => _popupService.ShowAdPopup();

        private void OnClickWatchAd() => _aDService.ShowAd();

        private void OnAdWatched() => InitializeGame();
    }
}