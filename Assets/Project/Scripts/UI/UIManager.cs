using Assets.Project.Scripts.Data;
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
        private TMP_Text zoneText;
        [SerializeField]
        private TMP_Text wheelTitleText;
        [SerializeField]
        private TMP_Text zoneBonusText;

        [SerializeField]
        private Button spinButton;
        [SerializeField]
        private Button collectButton;
        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private GameObject gameOverPanel;

        [SerializeField]
        private Image wheelImage;
        [SerializeField]
        private Image pointerImage;

        public Action OnSpinPressed;
        public Action OnCollectPressed;
        public Action OnRestartPressed;

        [SerializeField]
        private WheelView wheelView;

        [SerializeField] 
        private Transform rewardArea;

        [SerializeField] private GameObject rewardPrefab;
        List<CollectableRewardUI> currentRewards = new List<CollectableRewardUI>();

        private void OnValidate()
        {
            if (zoneText == null)
                zoneText = transform.Find("ui_panel_top/ui_text_zone_value")
                    .GetComponent<TMP_Text>();


            if (wheelTitleText == null)
                wheelTitleText = transform.Find("ui_panel_middle/ui_text_wheel_type")
                    .GetComponent<TMP_Text>();

            if (zoneBonusText == null)
                zoneBonusText = transform.Find("ui_panel_middle/ui_text_wheel_bonus")
                    .GetComponent<TMP_Text>();

            if (collectButton == null)
                collectButton = transform.Find("ui_panel_bottom/ui_button_collect")
                    .GetComponent<Button>();


        }

        private void Start()
        {
            spinButton.onClick.AddListener(
                () => OnSpinPressed?.Invoke());
            collectButton.onClick.AddListener(
                () => OnCollectPressed?.Invoke());
            restartButton.onClick.AddListener(
                () => OnRestartPressed?.Invoke());
        }

        public void UpdateZone(int zone)
        {
            zoneText.text = $"Zone: {zone}";
        }

        public void SetCollectButton(bool active)
        {
            collectButton.interactable = active;
        }

        public void SetSpinButton(
            bool active)
        {
            spinButton.interactable =
                active;
        }

        public void SetWheelRewardUI(int index, Sprite sprite, string text)
        {
            wheelView.SetRewardView(index, sprite, text);
        }

        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }

        public void WheelTypeText(string wheelType, Color textColor)
        {
            wheelTitleText.text = wheelType + "SPIN";
            wheelTitleText.color = textColor; 
        }

        public void ZoneBonusText(string bonusText = null)
        {
            if (bonusText != null)
            {
                zoneBonusText.gameObject.SetActive(true);
                zoneBonusText.text = "Up To " + bonusText + "Rewards";
            }
            else
            {
                zoneBonusText.gameObject.SetActive(false);
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

            CollectableRewardUI rewardUIEntity = Instantiate(rewardPrefab, rewardArea).GetComponent<CollectableRewardUI>();
            currentRewards.Add(rewardUIEntity);
            rewardUIEntity.Setup(rewardType, rewardSprite, rewardAmount);
        }

    }
}