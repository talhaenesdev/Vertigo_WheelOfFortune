namespace Assets.Project.Scripts.GamePlay
{
    public static class WheelRules
    {
        public static bool IsBombRestrictedZone(int zoneIndex)
        {
            return zoneIndex % 5 == 4;
        }
    }
}
