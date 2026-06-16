using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.GamePlay
{
    public class ZoneManager : MonoBehaviour
    {
        [SerializeField]
        private WheelConfig _wheelConfig;

        internal int CurrentZone { get; private set; }

        private void Start()
        {
            CurrentZone = _wheelConfig.StartZoneValue;
        }

        internal ZoneType GetCurrentZoneType()
        {
            if (CurrentZone % _wheelConfig.GoldenZoneSlice == 0)
                return ZoneType.Golden;

            if (CurrentZone % _wheelConfig.SilverZoneSlice == 0)
                return ZoneType.Silver;

            return ZoneType.Bronze;
        }

        internal void NextZone() => CurrentZone++;

        internal void ResetZone() => CurrentZone = _wheelConfig.StartZoneValue;

        internal int CurrentZoneIndex() => (CurrentZone - 1) % _wheelConfig.Zones.Count;

        internal bool CanCollect()
        {
            bool canCollect =
                GetCurrentZoneType() == ZoneType.Silver ||
                GetCurrentZoneType() == ZoneType.Golden;
            return canCollect;  
        }

        public WheelConfig GetWheelConfig() => _wheelConfig;
        public List<WheelSliceData> GetWheelSliceRows() => GetWheelConfig().Zones[CurrentZoneIndex()].Slices;
    }
}
