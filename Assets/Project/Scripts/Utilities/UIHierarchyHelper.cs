using UnityEngine;

public static class UIHierarchyHelper
{
    public static T FindComponent<T>(Transform root, string path) where T : Component
    {
        Transform target = root.Find(path);

        if (target == null)
        {
            Debug.LogError($"Path not found: {path}");
            return null;
        }

        T component = target.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError($"Component {typeof(T).Name} not found on {path}");
        }

        return component;
    }

    public static GameObject FindGameObject(Transform root, string path)
    {
        Transform target = root.Find(path);

        if (target == null)
        {
            Debug.LogError($"Path not found: {path}");
            return null;
        }

        return target.gameObject;
    }
}