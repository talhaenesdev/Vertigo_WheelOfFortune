using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.Scripts.GamePlay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private WheelController wheelController;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private ZoneManager zoneManager;


        [SerializeField]
        private WheelConfig normalWheel;

        [SerializeField]
        private WheelConfig silverWheel;

        [SerializeField]
        private WheelConfig goldenWheel;

        private GameState currentState;

        private void Start()
        {
            currentState = GameState.WaitingForInput;

            uiManager.OnSpinPressed += HandleSpin;
            uiManager.OnCollectPressed += HandleCollect;
            uiManager.OnRestartPressed += HandleRestart;

            UpdateLeaveButton();
            SetupWheel();
        }

        private void HandleRestart()
        {
            SceneManager.LoadScene(0);
        }

        private void HandleCollect()
        {
            if (currentState != GameState.WaitingForInput)
                return;

            ZoneType zoneType =
                zoneManager.GetCurrentZoneType();

            bool canCollect =
                zoneType == ZoneType.Safe ||
                zoneType == ZoneType.Super;

            if (!canCollect)
                return;


            currentState = GameState.GameOver;
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
            Debug.Log("Slice Type : " + result.SliceType);
            if (result.SliceType == SliceType.Bomb)
            {
                currentState = GameState.GameOver;

                rewardManager.ResetReward();

                uiManager.ShowGameOver();
                
                return;
            }

            rewardManager.AddReward(result.Reward);

            RewardType rewardType = result.Reward.RewardType;

            uiManager.AddRewardArea(rewardType,
                rewardManager.GetRewardIcon(rewardType),
                rewardManager.GetRewardAmount(rewardType));

            zoneManager.NextZone();

            uiManager.UpdateZone(zoneManager.CurrentZone);

            UpdateLeaveButton();

            uiManager.SetSpinButton(true);

            currentState = GameState.WaitingForInput;
        }

        private void SetupWheel()
        {
            ZoneType zoneType =
                zoneManager.GetCurrentZoneType();

            Debug.Log(
                $"Zone {zoneManager.CurrentZone} - {zoneType}");
            Debug.Log($"SetupWheel: {normalWheel.Zones[zoneManager.CurrentZone-1].Slices.Count}");

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

            uiManager.ZoneBonusText();

            switch (zoneType)
            {
                case ZoneType.Normal:
                    uiManager.WheelTypeText("Normal Wheel", Color.white);
                    wheelController.SetConfig(normalWheel);
                    break;

                case ZoneType.Safe:
                    uiManager.WheelTypeText("Safe Wheel", Color.green);
                    //wheelController.SetConfig(silverWheel);
                    break;

                case ZoneType.Super:
                    uiManager.WheelTypeText("Super Wheel", Color.yellow);
                    uiManager.ZoneBonusText("10x");
                    //wheelController.SetConfig(goldenWheel);
                    break;
            }
        }

      
        private void UpdateLeaveButton()
        {
            ZoneType zoneType = zoneManager.GetCurrentZoneType();

            bool canCollect =
                zoneType == ZoneType.Safe ||
                zoneType == ZoneType.Super;

            uiManager.SetCollectButton(canCollect);
        }

    }
}