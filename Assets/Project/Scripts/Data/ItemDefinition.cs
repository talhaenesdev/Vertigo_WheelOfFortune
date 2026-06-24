using UnityEngine;

namespace Assets.Project.Scripts.Data
{
    [CreateAssetMenu(
        fileName = "ItemDefinition",
        menuName = "Game/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _icon;

        public string Id => _id;
        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
    }
}