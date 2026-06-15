using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Economy;
using Assets.Project.Scripts.Enums;
using Assets.Project.Scripts.GamePlay;
using Assets.Project.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private WheelController wheelController;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private ZoneManager zoneManager;
        [SerializeField] private PopupManager _popupManager;
        [SerializeField] private CurrencyManager _currencyManager;
        [SerializeField] private ADManager _adManager;


        [SerializeField]
        private WheelConfig normalWheel;

        [SerializeField]
        private WheelVisualConfig wheelVisualConfig;

        [SerializeField]
        private ReviveData _reviveData;

        [SerializeField]
        private UserCurrencyData _userCurrencyData;
        
        private GameState currentState;

        private void Start()
        {
            uiManager.OnSpinPressed += HandleSpin;
            uiManager.OnCollectPressed += HandleCollect;
            uiManager.OnRestartPressed += HandleRestart;
            uiManager.OnWhatchAdReviveButton += HandleWhatchAdRevive;
            uiManager.OnCoinReviveButton += HandleCoinRevive;
            uiManager.OnOpenInventoryButton += OnOpenInventory;
            _popupManager.OnWatchAd += OnClickWatchAd;
            _adManager.OnCollectAdReward += OnAdWatched;  
            InitializeGame();
        }


        private void OnDestroy()
        {
            uiManager.OnSpinPressed -= HandleSpin;
            uiManager.OnCollectPressed -= HandleCollect;
            uiManager.OnRestartPressed -= HandleRestart;
            uiManager.OnWhatchAdReviveButton -= HandleWhatchAdRevive;
            uiManager.OnCoinReviveButton -= HandleCoinRevive;
            uiManager.OnOpenInventoryButton -= OnOpenInventory;
            _popupManager.OnWatchAd -= OnClickWatchAd;
            _adManager.OnCollectAdReward -= OnAdWatched;
        }

        private void InitializeGame()
        {
            currentState = GameState.WaitingForInput;
            uiManager.SetGameOverPanel(false);
            UpdateCollectButton();
            SetupWheel();
            SetWheelVisual();
            uiManager.SetSpinButton(true);

        }

        private void HandleRestart()
        {
            zoneManager.ResetZone();
            uiManager.UpdateZone(zoneManager.CurrentZone);
            uiManager.SetGameOverPanel(false);
            uiManager.ClearRewardArea();
            rewardManager.ResetReward();
            uiManager.SetSpinButton(true);
            InitializeGame();
        }

        private void HandleCollect()
        {
            if (currentState != GameState.WaitingForInput)
                return;

            ZoneType zoneType =
                zoneManager.GetCurrentZoneType();

            bool canCollect =
                zoneType == ZoneType.Bronze ||
                zoneType == ZoneType.Golden;

            if (!canCollect)
                return;

            currentState = GameState.GameOver;

            rewardManager.CollectReward();
            //The rewards will be added to the user’s inventory.
            HandleRestart(); // 
        }

        private void HandleSpin()
        {
            if (currentState != GameState.WaitingForInput)
                return;

            currentState = GameState.Spinning;

            uiManager.SetSpinButton(false);

            SetupWheel();

            int currentZoneIndex = zoneManager.CurrentZone - 1; 

            wheelController.Spin(OnSpinCompleted, currentZoneIndex);
        }

        private void OnSpinCompleted(
                WheelSliceData result)
        {
            if (result.SliceType == SliceType.Bomb)
            {
                currentState = GameState.Limbo;

                uiManager.SetGameOverPanel(true);

                return;
            }

            rewardManager.AddReward(result.Reward);

            RewardType rewardType = result.Reward.RewardType;

            uiManager.AddRewardArea(rewardType,
                rewardManager.GetRewardIcon(rewardType),
                rewardManager.GetRewardAmount(rewardType));

            zoneManager.NextZone();

            uiManager.UpdateZone(zoneManager.CurrentZone);

            UpdateCollectButton();

            uiManager.SetSpinButton(true);

            currentState = GameState.WaitingForInput;

            SetWheelVisual();
        }

        private void SetWheelVisual()
        {
            uiManager.SetWheelVisual(wheelVisualConfig.GetVisual(zoneManager.GetCurrentZoneType()));

            ZoneType zoneType =
                zoneManager.GetCurrentZoneType();

            uiManager.ZoneBonusText();

            switch (zoneType)
            {
                case ZoneType.Silver:
                    uiManager.WheelTypeText("Normal Wheel", Color.white);
                    break;

                case ZoneType.Bronze:
                    uiManager.WheelTypeText("Safe Wheel", Color.green);
                    break;

                case ZoneType.Golden:
                    uiManager.WheelTypeText("Super Wheel", Color.yellow);
                    uiManager.ZoneBonusText("10x");
                    break;
            }

            int trueZoneIndex = zoneManager.CurrentZone - 1;
            var slices = normalWheel.Zones[trueZoneIndex].Slices;

            for (int i = 0; i < slices.Count; i++)
            {
                var slice = slices[i];

                Sprite rewardIcon = null;
                string rewardAmount = string.Empty;

                if (slice.SliceType != SliceType.Bomb)
                {
                    rewardIcon = rewardManager.GetRewardIcon(slice.Reward.RewardType);
                    rewardAmount = slice.Reward.Amount.ToString();
                }
                else
                {
                    rewardIcon = rewardManager.GetRewardIcon(RewardType.Bomb);
                }

                uiManager.SetWheelRewardUI(i, rewardIcon, rewardAmount);
            }

            Debug.Log($"Zone {zoneManager.CurrentZone} - {zoneType}");
        }

        private void SetupWheel()
        {
            wheelController.SetConfig(normalWheel);
        }
      
        private void UpdateCollectButton()
        {
            ZoneType zoneType = zoneManager.GetCurrentZoneType();

            bool canCollect =
                zoneType == ZoneType.Bronze ||
                zoneType == ZoneType.Golden;

            uiManager.SetCollectButton(canCollect);
        }

        private void HandleCoinRevive()
        {
            if (_userCurrencyData.Coins >= _reviveData.CoinReviveCost)
            {
                _userCurrencyData.Coins -= _reviveData.CoinReviveCost;
                InitializeGame();
            }
            else
            {
                _popupManager.ShowNotEnoughCoinsPopup();
            }
        }

        private void HandleWhatchAdRevive()
        {
            _popupManager.ShowAdPopup();
        }

        private void OnClickWatchAd()
        {
            _adManager.ShowAd();
        }

        private void OnAdWatched()
        {
            InitializeGame();
        }
        private void OnOpenInventory()
        {
            Dictionary<RewardType, int> inventory = new Dictionary<RewardType, int>();

            inventory = rewardManager.GetRewardData();

            List<InventoryItemVO> inventoryItems = new List<InventoryItemVO>();

            foreach (var item in inventory)
            {
                inventoryItems.Add(new InventoryItemVO
                {
                    RewardType = item.Key,
                    RewardSprite = rewardManager.GetRewardIcon(item.Key),
                    RewardAmount = item.Value
                });
            }

            uiManager.OpenInventoryPanel(inventoryItems);
        }
    }
}