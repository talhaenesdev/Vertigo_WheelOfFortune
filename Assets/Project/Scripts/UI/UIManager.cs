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
        private TMP_Text _wheelTitleText;
        [SerializeField]
        private TMP_Text _zoneBonusText;
        [SerializeField]
        private TMP_Text _currencyText;

        [SerializeField]
        private Button _spinButton;
        [SerializeField]
        private Button _collectButton;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _whatchAdReviveButton;
        [SerializeField]
        private Button _coinReviveButton;
        [SerializeField]
        private Button _openInventoryButton;

        [SerializeField]
        private GameObject _gameOverPanel;

        [SerializeField]
        private Image _wheelImage;
        [SerializeField]
        private Image _pointerImage;

        [SerializeField]
        private WheelUI _wheelUI;
        [SerializeField]
        private Transform _rewardArea;


        [SerializeField] private GameObject _rewardPrefab;
        [SerializeField] private InventoryUI _inventoryView;
        [SerializeField] private RewardIconDatabase _rewardIconDatabase;
        [SerializeField] private WheelVisualConfig wheelVisualConfig;

        private List<CollectableRewardUI> _currentRewards = new List<CollectableRewardUI>();

        public Action OnSpinPressed;
        public Action OnCollectPressed;
        public Action OnRestartPressed;
        public Action OnWhatchAdReviveButton;
        public Action OnCoinReviveButton;
        public Action OnOpenInventoryButton;

        private void OnValidate() => AutoAssignReferences();
        private void Start() => SubscribeEvents();
        private void OnDisable() => UnSubscribeEvents();
        private void AutoAssignReferences()
        {
            if (_zoneText == null)
                _zoneText = transform.Find("ui_panel_top/ui_text_zone_value")
                    .GetComponent<TMP_Text>();

            if (_wheelTitleText == null)
                _wheelTitleText = transform.Find("ui_panel_middle/ui_text_wheel_type")
                    .GetComponent<TMP_Text>();

            if (_zoneBonusText == null)
                _zoneBonusText = transform.Find("ui_panel_middle/ui_text_wheel_bonus")
                    .GetComponent<TMP_Text>();

            if (_spinButton == null)
                _spinButton = transform.Find("ui_panel_bottom/ui_button_spin")
                    .GetComponent<Button>();

            if (_restartButton == null)
                _restartButton = transform.Find("ui_panel_gameover/ui_buttons/ui_button_giveup")
                    .GetComponent<Button>();

            if (_collectButton == null)
                _collectButton = transform.Find("ui_panel_bottom/ui_button_collect")
                    .GetComponent<Button>();

            if (_whatchAdReviveButton == null)
                _whatchAdReviveButton = transform.Find("ui_panel_gameover/ui_buttons/ui_button_revive_ad")
                    .GetComponent<Button>();

            if (_coinReviveButton == null)
                _coinReviveButton = transform.Find("ui_panel_gameover/ui_buttons/ui_button_revive_coin")
                    .GetComponent<Button>();

            if (_openInventoryButton == null)
                _openInventoryButton = transform.Find("ui_panel_top/ui_button_inventory")
                    .GetComponent<Button>();

            if (_gameOverPanel == null)
                _gameOverPanel = transform.Find("ui_panel_gameover").gameObject;

            if (_wheelImage == null)
                _wheelImage = transform.Find("ui_panel_middle/ui_image_wheel").GetComponent<Image>();

            if (_pointerImage == null)
                _pointerImage = transform.Find("ui_panel_middle/ui_image_pointer").GetComponent<Image>();

            if (_wheelUI == null)
                _wheelUI = transform.Find("ui_panel_middle/ui_image_wheel").GetComponent<WheelUI>();

            if (_rewardArea == null)
                _rewardArea = transform.Find("ui_panel_top/ui_panel_reward_area/ui_scroll_view/ui_view_port/ui_content").GetComponent<Transform>();
        }
        private void SubscribeEvents()
        {
            _spinButton.onClick.AddListener(
                () => OnSpinPressed?.Invoke());
            _collectButton.onClick.AddListener(
                () => OnCollectPressed?.Invoke());
            _restartButton.onClick.AddListener(
                () => OnRestartPressed?.Invoke());
            _whatchAdReviveButton.onClick.AddListener(
                () => OnWhatchAdReviveButton?.Invoke());
            _coinReviveButton.onClick.AddListener(
                () => OnCoinReviveButton?.Invoke());
            _openInventoryButton.onClick.AddListener(
                () => OnOpenInventoryButton?.Invoke());
        }
        private void UnSubscribeEvents()
        {
            _spinButton.onClick.RemoveListener(
                () => OnSpinPressed?.Invoke());
            _collectButton.onClick.RemoveListener(
                () => OnCollectPressed?.Invoke());
            _restartButton.onClick.RemoveListener(
                () => OnRestartPressed?.Invoke());
            _whatchAdReviveButton.onClick.RemoveListener(
                () => OnWhatchAdReviveButton?.Invoke());
            _coinReviveButton.onClick.RemoveListener(
                () => OnCoinReviveButton?.Invoke());
            _openInventoryButton.onClick.RemoveListener(
                () => OnOpenInventoryButton?.Invoke());
        }
        internal void UpdateZone(int zone) =>_zoneText.text = "ZONE " + zone;
        internal void SetCollectButtonInteractable(bool active) => _collectButton.interactable = active;
        internal void SetSpinButtonInteractable(bool active) => _spinButton.interactable = active;
        private void SetWheelRewardUI(int index, Sprite sprite, string text) => _wheelUI.SetRewardView(index, sprite, text);
        internal void SetGameOverPanel(bool isActive) => _gameOverPanel.SetActive(isActive);

        private void WheelTypeText(string wheelType, Color textColor)
        {
            _wheelTitleText.text = wheelType + " SPIN";
            _wheelTitleText.color = textColor; 
        }

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

            CollectableRewardUI rewardUIEntity = Instantiate(_rewardPrefab, _rewardArea).GetComponent<CollectableRewardUI>();
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
            _inventoryView.ShowInventory();
            _inventoryView.FillItemArea(inventoryItems);
        }

        internal void UpdateCurrencyText(int amount) => _currencyText.text = amount.ToString();

        internal Sprite GetRewardIcon(RewardType rewardType)
        {
            return _rewardIconDatabase.GetIcon(rewardType);
        }

        internal void SetWheelVisual(ZoneType currentZoneType, List<WheelSliceData> slices)
        {

            WheelTypeText(
                wheelVisualConfig.GetWheelTypeText(currentZoneType),
                wheelVisualConfig.GetWheelTextColor(currentZoneType));

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


                SetWheelRewardUI(i, rewardIcon, rewardAmount);

                WheelVisualVO wheelVisualVO = wheelVisualConfig.GetVisual(currentZoneType);

                _wheelUI.SetWheelVisuals(wheelVisualVO.PointerSprite,
                                         wheelVisualVO.WheelSprite,
                                         wheelVisualVO.TextColor);
            }
        }
    }
}