using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Game/Wheel Config")]
    public class WheelConfig : ScriptableObject
    {
        public List<WheelSliceRow> Zones;


        [Range(0, 10)]
        public int FreeSpinCount;
        [Range(0, 10)]
        public float SpinTime;
        public int SilverZoneSlice;
        public int GoldenZoneSlice;
        [Range(-10, 10)]
        public int PointerPunchAngle; 
        [Range(0, 10)]
        public int PointerPunchVibrato;
        [Range(0, 10)]
        public int PointerPunchElasticity;
        [Range(0, 10)]
        public int StartZoneValue;
        [Range(0, 3f)]
        public float SliceCenterOffsetMultiplier; 
        [Range(0, 50f)]
        public float ExtraRotationOffset;
        [Range(0, 1f)]
        public float PointerResetDuration;

#if UNITY_EDITOR        
        public List<ItemDefinition> AvailableItems;
        public List<ItemDefinition> GameOverItems;
#endif

    }
}