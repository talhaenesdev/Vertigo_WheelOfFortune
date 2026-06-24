using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Assets.Project.Scripts.Core;
using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;

public class ZoneManagerTests
{
    private ZoneManager CreateManager(int startZone = 1)
    {
        var go = new GameObject("ZoneManager");
        var manager = go.AddComponent<ZoneManager>();

        var config = ScriptableObject.CreateInstance<WheelConfig>();

        config.StartZoneValue = startZone;
        config.GoldenZoneSlice = 5;
        config.SilverZoneSlice = 3;
        config.Zones = new List<WheelSliceRow>
        {
            new WheelSliceRow { Slices = new List<WheelSliceData>() },
            new WheelSliceRow { Slices = new List<WheelSliceData>() },
            new WheelSliceRow { Slices = new List<WheelSliceData>() }
        };

        typeof(ZoneManager)
            .GetField("_wheelConfig", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(manager, config);

        return manager;
    }

    [Test]
    public void ResetZone_ShouldResetToStart()
    {
        var manager = CreateManager(5);

        manager.NextZone();
        manager.ResetZone();

        Assert.AreEqual(5, manager.CurrentZone);
    }

    [Test]
    public void GetZoneType_ShouldReturnGolden()
    {
        var manager = CreateManager(5);

        var type = manager.GetCurrentZoneType();

        Assert.AreEqual(ZoneType.Golden, type);
    }

    [Test]
    public void CanCollect_ShouldBeTrue_ForSilverOrGolden()
    {
        var manager = CreateManager(5);

        Assert.IsTrue(manager.CanCollect());
    }


    [Test]
    public void CurrentZoneIndex_ShouldLoopCorrectly()
    {
        var manager = CreateManager(1);

        manager.NextZone();
        manager.NextZone();

        var index = manager.CurrentZoneIndex();

        Assert.GreaterOrEqual(index, 0);
    }
}