using NUnit.Framework;
using UnityEngine;
using Assets.Project.Scripts.Economy;
using Assets.Project.Scripts.Data;

public class RewardManagerTests
{
    [Test]
    public void AddReward_ShouldIncreaseAmount()
    {
        var go = new GameObject();
        var manager = go.AddComponent<RewardManager>();

        var reward = new RewardData
        {
            RewardItem = CreateFakeItem("gold"),
            Amount = 10
        };

        manager.AddReward(reward);

        Assert.AreEqual(10, manager.GetRewardAmount("gold"));
    }

    private ItemDefinition CreateFakeItem(string id)
    {
        var obj = ScriptableObject.CreateInstance<ItemDefinition>();
        obj.name = id;


        typeof(ItemDefinition)
            .GetField("_id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(obj, id);

        return obj;
    }
    [Test]
    public void ResetReward_ShouldClearAllRewards()
    {
        var go = new UnityEngine.GameObject();
        var manager = go.AddComponent<RewardManager>();

        manager.AddReward(new RewardData
        {
            RewardItem = CreateFakeItem("gold"),
            Amount = 10
        });

        manager.ResetReward();

        Assert.AreEqual(0, manager.GetRewardAmount("gold"));
    }
    [Test]
    public void AddReward_ShouldNotCrash_WhenRewardIsValid()
    {
        var go = new UnityEngine.GameObject();
        var manager = go.AddComponent<RewardManager>();

        var reward = new RewardData
        {
            RewardItem = CreateFakeItem("gold"),
            Amount = 5
        };

        Assert.DoesNotThrow(() => manager.AddReward(reward));
    }
}