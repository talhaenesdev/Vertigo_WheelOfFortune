using System.Collections.Generic;
using UnityEngine;


namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "WheelVisualConfig", menuName = "Game/Wheel Visual Config")]
    public class WheelVisualConfig : ScriptableObject
    {
       public List<WheelVisualVO> WheelVisuals;
    }
}
