#if UNITY_EDITOR

using Assets.Project.Scripts.Data;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WheelConfig))]
public class WheelConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Zones"))
        {
            WheelConfig config =
                (WheelConfig)target;

            WheelGeneratorUtility.GenerateZones(
                config.Zones,
                config.AvailableItems,
                config.GameOverItems);

            EditorUtility.SetDirty(config);
        }
    }
}

#endif