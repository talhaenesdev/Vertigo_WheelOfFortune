using Assets.Project.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    internal class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;

        [SerializeField] private Transform _itemParent;
        [SerializeField] private GameObject _inventoryObjectPrefab;

        [SerializeField] private Button _closeButton;

        List<CollectableRewardUI> _pool = new();
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void SubscribeEvents() => _closeButton.onClick.AddListener(HideInventory);

        private void UnSubscribeEvents() => _closeButton.onClick.RemoveListener(HideInventory);

        internal void ShowInventory() => _inventoryPanel.SetActive(true);
        internal void FillItemArea(List<InventoryItemVO> inventoryItems)
        {
            ClearItemArea();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                CollectableRewardUI item;

                if (i < _pool.Count)
                {
                    item = _pool[i];
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item = Instantiate(_inventoryObjectPrefab, _itemParent)
                        .GetComponent<CollectableRewardUI>();

                    _pool.Add(item);
                }

                item.Setup(
                    inventoryItems[i].RewardType,
                    inventoryItems[i].RewardSprite,
                    inventoryItems[i].RewardAmount
                );
            }
        }

        private void ClearItemArea()
        {
            foreach (var item in _pool)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void HideInventory() 
        {
            _inventoryPanel.SetActive(false);
            ClearItemArea();
        }
    }
}
