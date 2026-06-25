
using Assets.Project.Scripts.Data;
using Assets.Project.Scripts.Economy;
using Assets.Project.Scripts.GamePlay;
using Assets.Project.Scripts.UI;
using UnityEngine;

namespace Assets.Project.Scripts.Core
{
    internal class GameInstaller : MonoBehaviour
    {
        [SerializeField] private RewardManager RewardManager;
        [SerializeField] private ZoneManager ZoneManager;
        [SerializeField] private UIManager UIManager;
        [SerializeField] private WheelController WheelController;
        [SerializeField] private PopupManager PopupManager;
        [SerializeField] private CurrencyManager CurrencyManager;
        [SerializeField] private ADManager ADManager;

        [SerializeField] private GameManager GameManager;

        [SerializeField] private ItemDatabase _itemDatabase;
        [SerializeField] private ReviveData _reviveData;
        [SerializeField] private WheelVisualConfig _wheelVisualConfig;
        [SerializeField] private ADData _adData;
        [SerializeField] private WheelConfig _wheelConfig;
        private void Awake()
        {
            GameManager.Inject(RewardManager, ZoneManager, UIManager, WheelController, PopupManager, CurrencyManager, ADManager, _itemDatabase, _reviveData);
            UIManager.Inject(_wheelVisualConfig);
            ADManager.Inject(_adData);
            ZoneManager.Inject(_wheelConfig);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!RewardManager)
                RewardManager = FindFirstObjectByType<RewardManager>();

            if (!ZoneManager)
                ZoneManager = FindFirstObjectByType<ZoneManager>();

            if (!UIManager)
                UIManager = FindFirstObjectByType<UIManager>();

            if (!WheelController)
                WheelController = FindFirstObjectByType<WheelController>();

            if (!PopupManager)
                PopupManager = FindFirstObjectByType<PopupManager>();

            if (!CurrencyManager)
                CurrencyManager = FindFirstObjectByType<CurrencyManager>();

            if (!ADManager)
                ADManager = FindFirstObjectByType<ADManager>();

            if (!GameManager)
                GameManager = FindFirstObjectByType<GameManager>();
        }
#endif
    }
}
