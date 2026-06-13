using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Button leaveButton;

        [SerializeField]
        private TMP_Text zoneText;

        [SerializeField]
        private TMP_Text rewardText;

        [SerializeField]
        private Button spinButton;
        [SerializeField]
        private Button collectButton;
        [SerializeField]
        private Button restarttButton;

        [SerializeField]
        private GameObject gameOverPanel;

        public System.Action OnSpinPressed;
        public System.Action OnCollectPressed;
        public System.Action OnRestartPressed;

        private void OnValidate()
        {
            if (zoneText == null)
                zoneText =
                    transform.Find(
                        "ui_panel_top/ui_text_zone_value")
                    .GetComponent<TMP_Text>();

            if (rewardText == null)
                rewardText =
                    transform.Find(
                        "ui_panel_top/ui_text_revard_value")
                    .GetComponent<TMP_Text>();

            if (leaveButton == null)
                leaveButton = transform.Find("ui_panel_buttons/ui_button_leave")
                    .GetComponent<Button>();
        }
        private void Start()
        {
            spinButton.onClick.AddListener(
                () => OnSpinPressed?.Invoke());
            collectButton.onClick.AddListener(
                () => OnCollectPressed?.Invoke());
            restarttButton.onClick.AddListener(
                () => OnRestartPressed?.Invoke());
        }

        public void UpdateZone(int zone)
        {
            zoneText.text = $"Zone: {zone}";
        }

        public void UpdateReward(int reward)
        {
            rewardText.text = $"Reward: {reward}";
        }
        public void SetLeaveButton(bool active)
        {
            leaveButton.interactable = active;
        }
        public void SetSpinButton(
            bool active)
        {
            spinButton.interactable =
                active;
        }
        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }
    }
}