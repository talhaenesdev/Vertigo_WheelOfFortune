using Assets.Project.Scripts.Enums;
using UnityEngine;
namespace Assets.Project.Scripts.GamePlay
{
    public class ZoneManager : MonoBehaviour
    {
        public int CurrentZone { get; private set; } = 1;

        public ZoneType GetCurrentZoneType()
        {
            if (CurrentZone % 30 == 0)
                return ZoneType.Golden;

            if (CurrentZone % 5 == 0)
                return ZoneType.Bronze;

            return ZoneType.Silver;
        }

        public void NextZone()
        {
            CurrentZone++;
        }

        public void ResetZone()
        {
            CurrentZone = 1;
        }
    }
}
