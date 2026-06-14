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

            int sliceCount = zone.Slices.Count;
            float sliceAngle = 360f / sliceCount;

            int targetIndex = Random.Range(0, sliceCount);

            float offset = sliceAngle * 0.9f;

            float targetRotation = 360f * 5 + (targetIndex * sliceAngle) - offset;

            wheelTransform
                .DORotate(
                    new Vector3(0, 0, -targetRotation),
                    4f,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    float z = wheelTransform.eulerAngles.z;

                    int resultIndex = Mathf.FloorToInt((z + offset) / sliceAngle) % sliceCount;

                    onComplete?.Invoke(zone.Slices[resultIndex]);
                });
        }
    }
}
