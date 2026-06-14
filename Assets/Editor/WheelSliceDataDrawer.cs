using UnityEditor;
using UnityEngine;
using Assets.Project.Scripts.Data;

[CustomPropertyDrawer(typeof(WheelSliceData))]
public class WheelSliceDataDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var sliceType = property.FindPropertyRelative("SliceType");

        bool isReward = (SliceType)sliceType.enumValueIndex == SliceType.Reward;

        return isReward
            ? EditorGUIUtility.singleLineHeight * 6
            : EditorGUIUtility.singleLineHeight * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var sliceType = property.FindPropertyRelative("SliceType");
        var reward = property.FindPropertyRelative("Reward");

        int index = GetLastArrayIndex(property.propertyPath);

        float lineHeight = EditorGUIUtility.singleLineHeight;

        Rect line = new Rect(position.x, position.y, position.width, lineHeight);

        // 🔥 Slice label
        EditorGUI.LabelField(line, index >= 0 ? $"Slice {index}" : "Slice");
        line.y += lineHeight;

        // SliceType
        EditorGUI.PropertyField(line, sliceType);
        line.y += lineHeight;

        bool isReward = (SliceType)sliceType.enumValueIndex == SliceType.Reward;

        if (isReward)
        {
            EditorGUI.PropertyField(line, reward, true);
        }
    }

    private int GetLastArrayIndex(string path)
    {
        if (string.IsNullOrEmpty(path))
            return -1;

        int end = path.LastIndexOf(']');
        int start = path.LastIndexOf('[', end);

        if (start >= 0 && end >= 0 && end > start)
        {
            string number = path.Substring(start + 1, end - start - 1);

            if (int.TryParse(number, out int result))
                return result;
        }

        return -1;
    }
}