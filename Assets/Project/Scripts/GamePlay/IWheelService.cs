using Assets.Project.Scripts.Data;

namespace Assets.Project.Scripts.GamePlay
{
    internal interface IWheelService
    {
        void SetConfig(WheelConfig config);
        void Spin(System.Action<WheelSliceData> onComplete, int currentZoneId);
    }
}
