using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "WheelVisualConfig", menuName = "Game/Wheel Visual Config")]
    public class WheelVisualConfig : ScriptableObject
    {
       [SerializeField] private List<WheelVisualVO> WheelVisuals;

       private Dictionary<ZoneType, WheelVisualVO> cache;

       private void OnEnable()
       {
           cache = new Dictionary<ZoneType, WheelVisualVO>();

           if (WheelVisuals == null)
               return;

           foreach (var visual in WheelVisuals)
           {
               if (visual == null)
                   continue;

               cache[visual.ZoneType] = visual;
           }
       }

        public WheelVisualVO GetVisual(ZoneType zoneType)
        {
            cache.TryGetValue(zoneType, out var visual);
            return visual;
        }
    }
}
