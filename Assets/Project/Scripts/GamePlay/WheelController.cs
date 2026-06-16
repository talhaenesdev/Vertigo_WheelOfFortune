using Assets.Project.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Assets.Project.Scripts.GamePlay
{
    internal class WheelController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _wheelTransform;

        private WheelConfig _currentConfig;

#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private void AutoAssignReferences()
        {
            if (_wheelTransform == null)
                _wheelTransform = UIHierarchyHelper.FindComponent<RectTransform>(
                    transform, "ui_panel_middle/ui_image_wheel_parent");
        }
#endif

        internal void SetConfig(WheelConfig config) => _currentConfig = config;

        internal void Spin(System.Action<WheelSliceData> onComplete, int currentZoneId)
        {
            var zone = _currentConfig.Zones[currentZoneId];

            int sliceCount = zone.Slices.Count;
            float sliceAngle = 360f / sliceCount;

            int targetIndex = Random.Range(0, sliceCount);

            float offset = sliceAngle * 0.9f;

            float targetRotation = 360f * _currentConfig.FreeSpinCount + (targetIndex * sliceAngle) - offset;

            _wheelTransform
                .DORotate(
                    new Vector3(0, 0, -targetRotation),
                    _currentConfig.SpinTime,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    float z = _wheelTransform.eulerAngles.z;

                    int resultIndex = Mathf.FloorToInt((z + offset) / sliceAngle) % sliceCount;

                    onComplete?.Invoke(zone.Slices[resultIndex]);
                });
        }

       
    }
}
