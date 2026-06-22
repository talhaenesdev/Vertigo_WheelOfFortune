
using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Project.Scripts.Core
{
    public interface IZoneService
    {
        int CurrentZone { get;}
        ZoneType GetCurrentZoneType();
        void NextZone();
        void ResetZone();
        int CurrentZoneIndex();
        bool CanCollect();
        WheelConfig GetWheelConfig();
        List<WheelSliceData> GetWheelSliceRows();

    }
}
