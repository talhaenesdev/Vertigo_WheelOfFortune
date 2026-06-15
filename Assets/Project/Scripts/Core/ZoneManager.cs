using Assets.Project.Scripts.Enums;
using UnityEngine;

namespace Assets.Project.Scripts.GamePlay
{
    public class ZoneManager : MonoBehaviour
    {
        internal int CurrentZone { get; private set; } = 1;

        internal ZoneType GetCurrentZoneType()
        {
            if (CurrentZone % 30 == 0)
                return ZoneType.Golden;

            if (CurrentZone % 5 == 0)
                return ZoneType.Silver;

            return ZoneType.Bronze;
        }

        internal void NextZone() => CurrentZone++;

        internal void ResetZone() => CurrentZone = 1;

        internal int CurrentZoneIndex() => CurrentZone - 1;

        internal bool CanCollect()
        {
            bool canCollect =
                GetCurrentZoneType() == ZoneType.Silver ||
                GetCurrentZoneType() == ZoneType.Golden;
            return canCollect;  
        }
    }
}
