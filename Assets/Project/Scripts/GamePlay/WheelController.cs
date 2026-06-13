using Assets.Project.Scripts.Data;
using UnityEngine;

namespace Assets.Project.Scripts.GamePlay
{
    public class WheelController : MonoBehaviour
    {
        private WheelConfig currentConfig;

        public void SetConfig(WheelConfig config)
        {
            currentConfig = config;
        }

        public WheelSliceData Spin()
        {
            int randomIndex =
                Random.Range(0, currentConfig.Slices.Count);

            return currentConfig.Slices[randomIndex];
        }
    }
}
