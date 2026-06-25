using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public static class WheelGeneratorUtility
{
    private const int SafeZoneStep = 5;

    public static void GenerateZones(
        List<WheelSliceRow> zones,
        List<ItemDefinition> availableItems,
         List<ItemDefinition> gameOverItems)
    {
        if (zones == null || availableItems == null || availableItems.Count == 0)
            return;

        for (int zoneIndex = 0; zoneIndex < zones.Count; zoneIndex++)
        {
            var zone = zones[zoneIndex];

            if (zone.Slices == null)
                continue;

            zone.Slices.Clear();

            int sliceCount = zone.Slices.Capacity > 0 ? zone.Slices.Capacity : 8;

            for (int i = 0; i < sliceCount; i++)
            {
                zone.Slices.Add(CreateReward(availableItems));
            }

            bool isSafeZone = (zoneIndex + 1) % SafeZoneStep == 0;

            if (isSafeZone)
                continue;

            int bombIndex = Random.Range(0, zone.Slices.Count);

            zone.Slices[bombIndex] = CreateBomb(gameOverItems);
        }
    }

    private static WheelSliceData CreateBomb(List<ItemDefinition> items)
    {
        var item = items[Random.Range(0, items.Count)];
        return new WheelSliceData
        {
            SliceType = SliceType.GameOver,
            Reward = new RewardData
            {
                RewardItem = item,
                Amount = 1
            }
        };
    }

    private static WheelSliceData CreateReward(List<ItemDefinition> items)
    {
        var item = items[Random.Range(0, items.Count)];

        return new WheelSliceData
        {
            SliceType = SliceType.Reward,
            Reward = new RewardData
            {
                RewardItem = item,
                Amount = Random.Range(1, 10)
            }
        };
    }
}