using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private TMP_Text _currencyText;

        [SerializeField] private Transform _itemParent;
        [SerializeField] private GameObject inventoryObjectPrefab;

        [SerializeField] private Button _closeButton;

        List<CollectableRewardUI> _inventoryRewards = new List<CollectableRewardUI>();
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(HideInventory);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(HideInventory);
        }

        public void ShowInventory()
        {
            _inventoryPanel.SetActive(true);
        }

        public void FillItemArea(List<InventoryItemVO> inventoryItems)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                CollectableRewardUI rewardUIEntity = Instantiate(inventoryObjectPrefab, _itemParent).GetComponent<CollectableRewardUI>();
                _inventoryRewards.Add(rewardUIEntity);
                rewardUIEntity.Setup(inventoryItems[i].RewardType, inventoryItems[i].RewardSprite, inventoryItems[i].RewardAmount);
            }
        }

        private void ClearItemArea()
        {
            foreach (var item in _inventoryRewards)
            {
                Destroy(item.gameObject);
            }
            _inventoryRewards.Clear();
        }
        private void HideInventory() 
        {
            _inventoryPanel.SetActive(false);
            ClearItemArea();
        }


    }
}
