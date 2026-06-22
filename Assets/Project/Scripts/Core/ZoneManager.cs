using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Core
{
    public class ZoneManager : MonoBehaviour, IZoneService
    {
        [SerializeField]
        private WheelConfig _wheelConfig;

        public int CurrentZone { get; private set; }

        private void Start()
        {
            CurrentZone = _wheelConfig.StartZoneValue;
        }

        public ZoneType GetCurrentZoneType()
        {
            if (CurrentZone % _wheelConfig.GoldenZoneSlice == 0)
                return ZoneType.Golden;

            if (CurrentZone % _wheelConfig.SilverZoneSlice == 0)
                return ZoneType.Silver;

            return ZoneType.Bronze;
        }

        public void NextZone() => CurrentZone++;

        public void ResetZone() => CurrentZone = _wheelConfig.StartZoneValue;

        public int CurrentZoneIndex() => (CurrentZone - 1) % _wheelConfig.Zones.Count;

        public bool CanCollect()
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
