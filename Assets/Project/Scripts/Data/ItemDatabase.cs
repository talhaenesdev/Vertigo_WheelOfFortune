using Assets.Project.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ItemDatabase",
    menuName = "Game/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemDefinition> _items;

    private Dictionary<string, ItemDefinition> _cache;

    private void OnEnable()
    {
        _cache = new Dictionary<string, ItemDefinition>();

        foreach (var item in _items)
        {
            if (item == null)
                continue;

            if (string.IsNullOrEmpty(item.Id))
                continue;

            _cache[item.Id] = item;
        }
    }

    public ItemDefinition GetById(string id)
    {
        if (_cache == null)
            OnEnable();

        _cache.TryGetValue(id, out var item);
        return item;
    }

    public Sprite GetIcon(string id)
    {
        var item = GetById(id);
        return item != null ? item.Icon : null;
    }
}