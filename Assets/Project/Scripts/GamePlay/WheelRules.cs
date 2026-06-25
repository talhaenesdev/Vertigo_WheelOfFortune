namespace Assets.Project.Scripts.GamePlay
{
    public static class WheelRules
    {
        public const int C_SafeZoneStep = 5;
        public const int C_BombOffset = 4;
        public static bool IsBombRestrictedZone(int zoneIndex)
        {
            return zoneIndex % C_SafeZoneStep == C_BombOffset;
        }
    }
}
