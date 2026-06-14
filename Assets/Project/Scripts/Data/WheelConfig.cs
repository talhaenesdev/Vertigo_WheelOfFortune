using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(
    fileName = "WheelConfig",
    menuName = "Game/Wheel Config")]
    public class WheelConfig : ScriptableObject
    {
        public List<WheelSliceRow> Zones;
    }
}