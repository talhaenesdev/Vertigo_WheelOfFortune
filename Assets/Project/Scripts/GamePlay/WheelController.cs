using Assets.Project.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Project.Scripts.GamePlay
{
    internal class WheelController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _wheelTransform;

        [SerializeField]
        private RectTransform _pointerTransform;

        private WheelConfig _currentConfig;

        internal void SetConfig(WheelConfig config) => _currentConfig = config;

        internal void Spin(System.Action<WheelSliceData> onComplete, int currentZoneId)
        {
            _pointerTransform.DOKill(true);
            _wheelTransform.DOKill(true);

            if (currentZoneId < 0 || currentZoneId >= _currentConfig.Zones.Count)
                return;

            var zone = _currentConfig.Zones[currentZoneId];

            int sliceCount = zone.Slices.Count;
            float sliceAngle = 360f / sliceCount;

            int targetIndex = Random.Range(0, sliceCount);

            float offset = sliceAngle * _currentConfig.SliceCenterOffsetMultiplier;

            float targetRotation =
                360f * _currentConfig.FreeSpinCount +
                (targetIndex * sliceAngle) +
                offset +
                _currentConfig.ExtraRotationOffset;

            _pointerTransform.DOPunchRotation(
                new Vector3(0, 0, -_currentConfig.PointerPunchAngle),
                _currentConfig.SpinTime,
                _currentConfig.PointerPunchVibrato,
                _currentConfig.PointerPunchElasticity
            );

            _wheelTransform
                .DORotate(
                    new Vector3(0, 0, -targetRotation),
                    _currentConfig.SpinTime,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    float z = Mathf.Repeat(_wheelTransform.eulerAngles.z, 360f);

                    int resultIndex =
                        Mathf.RoundToInt(z / sliceAngle) % sliceCount;

                    if (resultIndex < 0)
                        resultIndex += sliceCount;

                    onComplete?.Invoke(zone.Slices[resultIndex]);

                    _pointerTransform.DORotate(Vector3.zero, _currentConfig.PointerResetDuration);

                    _wheelTransform.localEulerAngles = Vector3.zero;
                });
        }

    }
}
