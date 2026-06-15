using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text _zoneText;
        [SerializeField]
        private TMP_Text _wheelTitleText;
        [SerializeField]
        private TMP_Text _zoneBonusText;

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
        private WheelView _wheelView;
        [SerializeField]
        private Transform _rewardArea;

        public Action OnSpinPressed;
        public Action OnCollectPressed;
        public Action OnRestartPressed;
        public Action OnWhatchAdReviveButton;
        public Action OnCoinReviveButton;
        public Action OnOpenInventoryButton;



        [SerializeField] private GameObject rewardPrefab;
        [SerializeField] private InventoryView _inventoryView;
        List<CollectableRewardUI> currentRewards = new List<CollectableRewardUI>();

        private void OnValidate()
        {
            AutoAssignReferences();
        }

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

            if (_wheelView == null)
                _wheelView = transform.Find("ui_panel_middle/ui_image_wheel").GetComponent<WheelView>();

            if (_rewardArea == null)
                _rewardArea = transform.Find("ui_panel_top/ui_panel_reward_area/ui_scroll_view/ui_view_port/ui_content").GetComponent<Transform>();
        }

        private void Start()
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

        public void UpdateZone(int zone)
        {
            _zoneText.text = $"Zone: {zone}";
        }

        public void SetCollectButton(bool active)
        {
            _collectButton.interactable = active;
        }

        public void SetSpinButton(bool active)
        {
            _spinButton.interactable = active;
        }

        public void SetWheelRewardUI(int index, Sprite sprite, string text)
        {
            _wheelView.SetRewardView(index, sprite, text);
        }

        public void SetGameOverPanel(bool isActive)
        {
            _gameOverPanel.SetActive(isActive);
        }
        public void WheelTypeText(string wheelType, Color textColor)
        {
            _wheelTitleText.text = wheelType + "SPIN";
            _wheelTitleText.color = textColor; 
        }

        public void ZoneBonusText(string bonusText = null)
        {
            if (bonusText != null)
            {
                _zoneBonusText.gameObject.SetActive(true);
                _zoneBonusText.text = "Up To " + bonusText + "Rewards";
            }
            else
            {
                _zoneBonusText.gameObject.SetActive(false);
            }
        }

        public void AddRewardArea(RewardType rewardType, Sprite rewardSprite, int rewardAmount)
        {
            foreach (var rewardUI in currentRewards)
            {
                if (rewardType == rewardUI.RewardType)
                {
                    rewardUI.SetAmountText(rewardAmount);
                    return;
                }
            }

            CollectableRewardUI rewardUIEntity = Instantiate(rewardPrefab, _rewardArea).GetComponent<CollectableRewardUI>();
            currentRewards.Add(rewardUIEntity);
            rewardUIEntity.Setup(rewardType, rewardSprite, rewardAmount);
        }

        public void ClearRewardArea()
        {
            foreach (var rewardUI in currentRewards)
            {
                Destroy(rewardUI.gameObject);
            }
            currentRewards.Clear();
        }

        public void SetWheelVisual(WheelVisualVO wheelVisualVO)
        {
            _wheelView.SetPointerImage(wheelVisualVO.PointerSprite, wheelVisualVO.WheelSprite, wheelVisualVO.TextColor);
        }

        public void OpenInventoryPanel(List<InventoryItemVO> inventoryItems)
        {
            _inventoryView.ShowInventory();
            _inventoryView.FillItemArea(inventoryItems);
        }
    }
}