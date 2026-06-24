using NUnit.Framework;
using UnityEngine;
using Assets.Project.Scripts.GamePlay;
using Assets.Project.Scripts.Data;

public class WheelControllerTests
{
    private WheelController CreateWheel(WheelConfig config)
    {
        var go = new GameObject("Wheel");
        var wheel = go.AddComponent<WheelController>();
        wheel.SetConfig(config);
        return wheel;
    }

    private WheelConfig CreateTestConfig()
    {
        var config = ScriptableObject.CreateInstance<WheelConfig>();

        config.Zones = new System.Collections.Generic.List<WheelSliceRow>
        {
            new WheelSliceRow
            {
                Slices = new System.Collections.Generic.List<WheelSliceData>
                {
                    new WheelSliceData(),
                    new WheelSliceData(),
                    new WheelSliceData(),
                    new WheelSliceData()
                }
            }
        };

        config.FreeSpinCount = 1;
        config.SpinTime = 0.1f;
        config.SliceCenterOffsetMultiplier = 0;
        config.ExtraRotationOffset = 0;
        config.PointerPunchAngle = 10;
        config.PointerPunchVibrato = 5;
        config.PointerPunchElasticity = 1;
        config.PointerResetDuration = 0.1f;

        return config;
    }


    [Test]
    public void Spin_ShouldNotInvoke_WhenZoneInvalid()
    {
        var wheel = CreateWheel(CreateTestConfig());

        bool called = false;

        wheel.Spin((slice) =>
        {
            called = true;
        }, -1);

        Assert.IsFalse(called);
    }

   
    [Test]
    public void Spin_ShouldNotCrash_WhenConfigIsValid()
    {
        var go = new GameObject();
        var wheel = go.AddComponent<WheelController>();

        Assert.DoesNotThrow(() =>
        {
            wheel.SetConfig(CreateTestConfig());
            wheel.Spin((slice) => { }, 0);
        });
    }

    [Test]
    public void Spin_ShouldReturnSliceFromZone()
    {
        var wheel = CreateWheel(CreateTestConfig());

        wheel.Spin((slice) =>
        {
            Assert.IsNotNull(slice);
        }, 0);
    }
}