using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Game/Wheel Config")]
    internal class WheelConfig : ScriptableObject
    {
        public List<WheelSliceRow> Zones;
        public int FreeSpinCount;
        public float SpinTime;
    }
}