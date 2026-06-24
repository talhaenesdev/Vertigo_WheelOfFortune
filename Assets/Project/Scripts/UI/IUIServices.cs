
using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Scripts.UI
{
    internal interface IUIServices
    {
        event Action OnSpinPressed;
        event Action OnCollectPressed;
        event Action OnRestartPressed;
        event Action OnWatchAdRevive;
        event Action OnCoinRevive;
        event Action OnOpenInventory;
        void UpdateZone(int zone);
        void SetCollectButtonInteractable(bool active);
        void SetSpinButtonInteractable(bool active);
        void SetGameOverPanel(bool isActive);
        void AddRewardArea(string rewardId, Sprite rewardSprite, int rewardAmount);
        void ClearRewardArea();
        void OpenInventoryPanel(List<InventoryItemVO> inventoryItems);
        void UpdateCurrencyText(int amount);
        void SetWheelVisual(ZoneType currentZoneType, List<WheelSliceData> slices);
    }
}
