using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class UIManager : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text _zoneText;
        [SerializeField]
        private TMP_Text _currencyText;

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
        private GameObject _inventoryPanel;

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

        public Action OnSpinPressed;
        public Action OnCollectPressed;
        public Action OnRestartPressed;
        public Action OnWhatchAdReviveButton;
        public Action OnCoinReviveButton;
        public Action OnOpenInventoryButton;


#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private T FindComponent<T>(string path) where T : Component
        {
            Transform target = transform.Find(path);

            if (target == null)
            {
                Debug.LogError(
                    $"[{nameof(UIManager)}] Path not found: {path}",
                    this);

                return null;
            }

            T component = target.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError(
                    $"[{nameof(UIManager)}] Component {typeof(T).Name} not found on {path}",
                    this);
            }

            return component;
        }
        private GameObject FindGameObject(string path)
        {
            Transform target = transform.Find(path);

            if (target == null)
            {
                Debug.LogError($"Path not found: {path}", this);
                return null;
            }

            return target.gameObject;
        }
        private void AutoAssignReferences()
        {
            if (_zoneText == null)
                _zoneText = FindComponent<TMP_Text>(
                    "ui_panel_top/ui_text_zone_value");

            if (_currencyText == null)
                _currencyText = FindComponent<TMP_Text>(
                    "ui_panel_top/ui_text_currency_value");

            if (_spinButton == null)
                _spinButton = FindComponent<Button>(
                    "ui_panel_bottom/ui_button_spin");
            if (_restartButton == null)
                _restartButton = FindComponent<Button>(
                    "ui_panel_gameover/ui_buttons/ui_button_giveup");

            if (_collectButton == null)
                _collectButton = FindComponent<Button>(
                    "ui_panel_bottom/ui_button_collect");
            if (_watchAdReviveButton == null)
                _watchAdReviveButton = FindComponent<Button>(
                    "ui_panel_gameover/ui_buttons/ui_button_revive_ad");

            if (_coinReviveButton == null)
                _coinReviveButton = FindComponent<Button>(
                    "ui_panel_gameover/ui_buttons/ui_button_revive_coin");
            if (_openInventoryButton == null)
                _openInventoryButton = FindComponent<Button>(
                    "ui_panel_top/ui_button_inventory");

            if (_gameOverPanel == null)
                _gameOverPanel = FindGameObject("ui_panel_gameover");

            if (_inventoryPanel == null)
                _inventoryPanel = FindGameObject("ui_panel_inventory");
            
            if (_wheelUI == null)
                _wheelUI = FindComponent<WheelUI>(
                    "ui_panel_middle/ui_image_wheel");

            if (_inventoryUI == null)
                _inventoryUI = FindComponent<InventoryUI>(
                    "ui_panel_inventory");

            if (_rewardArea == null)
                _rewardArea = FindComponent<Transform>(
                    "ui_panel_top/ui_panel_reward_area/ui_scroll_view/ui_view_port/ui_content");
        }
#endif


        internal void UpdateZone(int zone) =>_zoneText.text = "ZONE " + zone;
        internal void SetCollectButtonInteractable(bool active) => _collectButton.interactable = active;
        internal void SetSpinButtonInteractable(bool active) => _spinButton.interactable = active;
        internal void SetGameOverPanel(bool isActive) => _gameOverPanel.SetActive(isActive);
        internal void AddRewardArea(RewardType rewardType, Sprite rewardSprite, int rewardAmount)
        {
            foreach (var rewardUI in _currentRewards)
            {
                if (rewardType == rewardUI.RewardType)
                {
                    rewardUI.SetAmountText(rewardAmount);
                    return;
                }
            }

            CollectableRewardUI rewardUIEntity = Instantiate(_inventoryPanel, _rewardArea).GetComponent<CollectableRewardUI>();
            _currentRewards.Add(rewardUIEntity);
            rewardUIEntity.Setup(rewardType, rewardSprite, rewardAmount);
        }

        internal void ClearRewardArea()
        {
            foreach (var rewardUI in _currentRewards)
            {
                Destroy(rewardUI.gameObject);
            }
            _currentRewards.Clear();
        }

        internal void OpenInventoryPanel(List<InventoryItemVO> inventoryItems)
        {
            _inventoryUI.ShowInventory();
            _inventoryUI.FillItemArea(inventoryItems);
        }

        internal void UpdateCurrencyText(int amount) => _currencyText.text = amount.ToString();

        internal void SetWheelVisual(ZoneType currentZoneType, List<WheelSliceData> slices)
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
        internal Sprite GetRewardIcon(RewardType rewardType)
        {
            return _rewardIconDatabase.GetIcon(rewardType);
        }
    }
}