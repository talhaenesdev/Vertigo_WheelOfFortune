using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Game/Wheel Config")]
    public class WheelConfig : ScriptableObject
    {
        public List<WheelSliceRow> Zones;
        public int FreeSpinCount;
        public float SpinTime;
        public int SilverZoneSlice;
        public int GoldenZoneSlice;
        public int PointerPunchAngle;
        public int PointerPunchVibrato;
        public int PointerPunchElasticity;
        public int StartZoneValue;
        public float SliceCenterOffsetMultiplier;
        public float ExtraRotationOffset;
        public float PointerResetDuration;

#if UNITY_EDITOR        
        public List<ItemDefinition> AvailableItems;
        public List<ItemDefinition> GameOverItems;
#endif

    }
}