using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.UI;
using System.Runtime.InteropServices;
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

            Debug.Log(
                $"PLAYER COLLECTED {rewardManager.TotalReward}");

            currentState = GameState.GameOver;
        }

        private void HandleSpin()
        {
            if (currentState != GameState.WaitingForInput)
                return;

            currentState = GameState.Spinning;

            uiManager.SetSpinButton(false);

            SetupWheel();

            wheelController.Spin(OnSpinCompleted);
        }
        private void OnSpinCompleted(
                WheelSliceData result)
        {
            if (result.SliceType == SliceType.Bomb)
            {
                currentState = GameState.GameOver;

                rewardManager.ResetReward();

                uiManager.ShowGameOver();
                Debug.Log("GAME OVER");

                return;
            }

            rewardManager.AddReward(result.RewardAmount);

            zoneManager.NextZone();

            uiManager.UpdateZone(zoneManager.CurrentZone);
            uiManager.UpdateReward(rewardManager.TotalReward);

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

            switch (zoneType)
            {
                case ZoneType.Normal:
                    wheelController.SetConfig(normalWheel);
                    break;

                case ZoneType.Safe:
                    wheelController.SetConfig(silverWheel);
                    break;

                case ZoneType.Super:
                    wheelController.SetConfig(goldenWheel);
                    break;
            }
        }

        private void PlayTurn()
        {
            Debug.Log(
                $"Zone Type: {zoneManager.GetCurrentZoneType()}");

            SetupWheel();

            WheelSliceData result = null;
               // wheelController.Spin();

            if (result.SliceType == SliceType.Bomb)
            {
                rewardManager.ResetReward();

                Debug.Log("BOMB!");
                Debug.Log("GAME OVER");

                CancelInvoke();

                return;
            }

            rewardManager.AddReward(result.RewardAmount);

            Debug.Log($"Reward: {result.RewardAmount}");
            Debug.Log($"Total: {rewardManager.TotalReward}");

            zoneManager.NextZone();

            uiManager.UpdateZone(
                zoneManager.CurrentZone);

            uiManager.UpdateReward(
                rewardManager.TotalReward);

            ZoneType zoneType = zoneManager.GetCurrentZoneType();

            bool canLeave =
                zoneType == ZoneType.Safe ||
                zoneType == ZoneType.Super;

            uiManager.SetLeaveButton(canLeave);
        }
        private void UpdateLeaveButton()
        {
            ZoneType zoneType = zoneManager.GetCurrentZoneType();

            bool canLeave =
                zoneType == ZoneType.Safe ||
                zoneType == ZoneType.Super;

            uiManager.SetLeaveButton(canLeave);
        }

    }
}