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

        public void Spin(System.Action<WheelSliceData> onComplete)
        {
            int targetIndex =
                Random.Range(0, currentConfig.Slices.Count);

            float sliceAngle =
                360f / currentConfig.Slices.Count;

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
                    onComplete?.Invoke(
                        currentConfig.Slices[targetIndex]);
                });
        }
    }
}
