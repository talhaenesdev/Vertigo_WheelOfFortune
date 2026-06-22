using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using Assets.Project.Scripts.Utilities;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class UIManager : MonoBehaviour, IUIServices
    {

        [SerializeField]
        private TMP_Text _zoneValue;
        [SerializeField]
        private TMP_Text _currencyValue;

        [SerializeField]
        private Button _spinButton;
        [SerializeField]
        private Button _collectButton;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _watchAdReviveButton;
        [SerializeField]
        private Button _coinReviveButton;
        [SerializeField]
        private Button _openInventoryButton;

        [SerializeField]
        private GameObject _gameOverPanel;
        [SerializeField] 
        private GameObject _rewardPrefab;

        [SerializeField]
        private WheelUI _wheelUI;
        [SerializeField]
        private InventoryUI _inventoryUI;

        [SerializeField]
        private Transform _rewardArea;


        [SerializeField] 
        private RewardIconDatabase _rewardIconDatabase;
        [SerializeField] 
        private WheelVisualConfig _wheelVisualConfig;

        private List<CollectableRewardUI> _currentRewards = new List<CollectableRewardUI>();

        public event Action OnSpinPressed;
        public event Action OnCollectPressed;
        public event Action OnRestartPressed;
        public event Action OnWhatchAdReviveButton;
        public event Action OnCoinReviveButton;
        public event Action OnOpenInventoryButton;


#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private void AutoAssignReferences()
        {
            if (!_zoneValue)
                _zoneValue = UIHierarchyHelper.FindComponent<TMP_Text>(
                    transform, "ui_panel_top/ui_text_zone_value");

            if (!_currencyValue)
                _currencyValue = UIHierarchyHelper.FindComponent<TMP_Text>(
                    transform, "ui_panel_top/ui_text_currency_value");

            if (!_spinButton)
                _spinButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_panel_bottom/ui_button_spin");

            if (!_restartButton)
                _restartButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_panel_gameover/ui_buttons/ui_button_giveup");

            if (!_collectButton)
                _collectButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_panel_bottom/ui_button_collect");

            if (!_watchAdReviveButton)
                _watchAdReviveButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_panel_gameover/ui_buttons/ui_button_revive_ad");

            if (!_coinReviveButton)
                _coinReviveButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_panel_gameover/ui_buttons/ui_button_revive_coin");

            if (!_openInventoryButton)
                _openInventoryButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_panel_top/ui_button_inventory");

            if (!_gameOverPanel)
                _gameOverPanel = UIHierarchyHelper.FindGameObject(
                    transform, "ui_panel_gameover");

            if (!_wheelUI)
                _wheelUI = UIHierarchyHelper.FindComponent<WheelUI>(
                    transform, "ui_panel_middle/ui_image_wheel");

            if (!_inventoryUI)
                _inventoryUI = UIHierarchyHelper.FindComponent<InventoryUI>(
                    transform, "ui_panel_inventory");

            if (!_rewardArea)
                _rewardArea = UIHierarchyHelper.FindComponent<Transform>(
                    transform, "ui_panel_top/ui_panel_reward_area/ui_scroll_view/ui_view_port/ui_content");

            if (!_rewardPrefab)
            {
                Debug.LogError($"[{nameof(UIManager)}] Reward prefab is missing!", this);
                return;
            }
        }
#endif
        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _spinButton.onClick.AddListener(OnSpinClicked);
            _collectButton.onClick.AddListener(OnCollectClicked);
            _restartButton.onClick.AddListener(OnRestartClicked);
            _watchAdReviveButton.onClick.AddListener(OnWatchAdReviveClicked);
            _coinReviveButton.onClick.AddListener(OnCoinReviveClicked);
            _openInventoryButton.onClick.AddListener(OnOpenInventoryClicked);
        }
        private void UnSubscribeEvents()
        {
            _spinButton.onClick.RemoveListener(OnSpinClicked);
            _collectButton.onClick.RemoveListener(OnCollectClicked);
            _restartButton.onClick.RemoveListener(OnRestartClicked);
            _watchAdReviveButton.onClick.RemoveListener(OnWatchAdReviveClicked);
            _coinReviveButton.onClick.RemoveListener(OnCoinReviveClicked);
            _openInventoryButton.onClick.RemoveListener(OnOpenInventoryClicked);
        }

        private void OnSpinClicked() => OnSpinPressed?.Invoke();
        private void OnCollectClicked() => OnCollectPressed?.Invoke();
        private void OnRestartClicked() => OnRestartPressed?.Invoke();
        private void OnWatchAdReviveClicked() => OnWhatchAdReviveButton?.Invoke();
        private void OnCoinReviveClicked() => OnCoinReviveButton?.Invoke();
        private void OnOpenInventoryClicked() => OnOpenInventoryButton?.Invoke();


        public void UpdateZone(int zone) =>_zoneValue.text = "ZONE " + zone;
        public void SetCollectButtonInteractable(bool active) => _collectButton.interactable = active;
        public void SetSpinButtonInteractable(bool active) => _spinButton.interactable = active;
        public void SetGameOverPanel(bool isActive) => _gameOverPanel.SetActive(isActive);
        public void AddRewardArea(RewardType rewardType, Sprite rewardSprite, int rewardAmount)
        {
            foreach (var rewardUI in _currentRewards)
            {
                if (rewardType == rewardUI.RewardType)
                {
                    rewardUI.SetAmountText(rewardAmount);
                    return;
                }
            }

            CollectableRewardUI rewardUIEntity = Instantiate(_rewardPrefab, _rewardArea).GetComponent<CollectableRewardUI>();
            _currentRewards.Add(rewardUIEntity);
            rewardUIEntity.Setup(rewardType, rewardSprite, rewardAmount);
        }

        public void ClearRewardArea()
        {
            foreach (var rewardUI in _currentRewards)
            {
                Destroy(rewardUI.gameObject);
            }
            _currentRewards.Clear();
        }

        public void OpenInventoryPanel(List<InventoryItemVO> inventoryItems)
        {
            _inventoryUI.ShowInventory();
            _inventoryUI.FillItemArea(inventoryItems);
        }

        public void UpdateCurrencyText(int amount) => _currencyValue.text = "GOLD : " + amount.ToString();

        public void SetWheelVisual(ZoneType currentZoneType, List<WheelSliceData> slices)
        {

            WheelVisualVO wheelVisualVO = _wheelVisualConfig.GetVisual(currentZoneType);

            _wheelUI.SetWheelVisuals(wheelVisualVO.PointerSprite,
                                     wheelVisualVO.WheelSprite,
                                     wheelVisualVO.TextColor,
                                     wheelVisualVO.WheelTypeText,
                                     wheelVisualVO.ZoneTypeText);


            for (int i = 0; i < slices.Count; i++)
            {
                var slice = slices[i];

                Sprite rewardIcon = null;
                string rewardAmount = string.Empty;

                if (slice.SliceType != SliceType.Bomb)
                {
                    rewardIcon = GetRewardIcon(slice.Reward.RewardType);
                    rewardAmount = slice.Reward.Amount.ToString();
                }
                else
                {
                    rewardIcon = GetRewardIcon(RewardType.Bomb);
                }

                _wheelUI.SetRewardView(i, rewardIcon, rewardAmount);
            }
        }
        public Sprite GetRewardIcon(RewardType rewardType)
        {
            return _rewardIconDatabase.GetIcon(rewardType);
        }
    }
}