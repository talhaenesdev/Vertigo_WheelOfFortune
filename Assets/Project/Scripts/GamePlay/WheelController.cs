using Assets.Project.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Project.Scripts.GamePlay
{
    public class WheelController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform wheelTransform;

        private WheelConfig currentConfig;

        public void SetConfig(WheelConfig config)
        {
            currentConfig = config;
        }

        public void Spin(System.Action<WheelSliceData> onComplete, int currentZoneId)
        {
            var zone = currentConfig.Zones[currentZoneId];

            int targetIndex =
                Random.Range(0, zone.Slices.Count);

            float sliceAngle =
                360f / zone.Slices.Count;

            float targetAngle =
                targetIndex * sliceAngle;

            float finalRotation =
                1440f + targetAngle;

            wheelTransform
                .DORotate(
                    new Vector3(0, 0, -finalRotation),
                    4f,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    onComplete?.Invoke(zone.Slices[targetIndex]);
                });

            Debug.Log(
                $"Spinning wheel for zone {currentZoneId} " +
                $"reward : {zone.Slices[targetIndex].Reward?.RewardType} " +
                $"amount : {zone.Slices[targetIndex].Reward?.Amount}");
        }
    }
}
