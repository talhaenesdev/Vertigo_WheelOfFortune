using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Assets.Project.Scripts.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;

        [SerializeField] private Transform _itemParent;
        private GameObject _inventoryObjectPrefab;

        [SerializeField] private Button _closeButton;

        List<CollectableRewardUI> _pool = new();
        private AsyncOperationHandle<GameObject> _inventoryPrefabHandle;
#if UNITY_EDITOR
        private void OnValidate() => AutoAssignReferences();
        private void AutoAssignReferences()
        {
            if (_inventoryPanel == null)
                _inventoryPanel = UIHierarchyHelper.FindGameObject(
                    transform, "ui_panel_inventory");

            if (_inventoryObjectPrefab == null)
                _inventoryObjectPrefab = UIHierarchyHelper.FindGameObject(
                    transform, "ui_panel_reward_area/ui_scroll_view/ui_view_port/ui_content");

            if (_closeButton == null)
                _closeButton = UIHierarchyHelper.FindComponent<Button>(
                    transform, "ui_button_close");
        }
#endif

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        public async Task Initialize()
        {
            _inventoryPrefabHandle =
                Addressables.LoadAssetAsync<GameObject>("ui_reward_main");

            await _inventoryPrefabHandle.Task;

            if (_inventoryPrefabHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return;
            }

            _inventoryObjectPrefab = _inventoryPrefabHandle.Result;
        }

        private void SubscribeEvents() => _closeButton.onClick.AddListener(HideInventory);

        private void UnSubscribeEvents() 
        {
            _closeButton.onClick.RemoveListener(HideInventory);
            if (_inventoryPrefabHandle.IsValid())
                Addressables.Release(_inventoryPrefabHandle);
        }

        public void ShowInventory() => _inventoryPanel.SetActive(true);
        public void FillItemArea(List<InventoryItemVO> inventoryItems)
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
                    inventoryItems[i].RewardId,
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
