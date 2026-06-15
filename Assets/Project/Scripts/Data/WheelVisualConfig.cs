using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "WheelVisualConfig", menuName = "Game/Wheel Visual Config")]
    internal class WheelVisualConfig : ScriptableObject
    {
       [SerializeField] private List<WheelVisualVO> _wheelVisuals;

       private Dictionary<ZoneType, WheelVisualVO> _cache;

       private void OnEnable()
       {
           _cache = new Dictionary<ZoneType, WheelVisualVO>();

           if (_wheelVisuals == null)
               return;

           foreach (var visual in _wheelVisuals)
           {
               if (visual == null)
                   continue;

               _cache[visual.ZoneType] = visual;
           }
       }

        internal WheelVisualVO GetVisual(ZoneType zoneType)
        {
            _cache.TryGetValue(zoneType, out var visual);
            return visual;
        }

        internal string GetWheelTypeText(ZoneType zoneType)
        {
            if (_cache.TryGetValue(zoneType, out var visual))
            {
                return visual.TitleText;
            }
            return string.Empty;
        }

        internal Color GetWheelTextColor(ZoneType zoneType)
        {
            if (_cache.TryGetValue(zoneType, out var visual))
            {
                return visual.TextColor;
            }
            return Color.white;
        }
    }
}
