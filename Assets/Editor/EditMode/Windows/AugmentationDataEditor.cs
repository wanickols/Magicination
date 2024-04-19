using MGCNTN;
using UnityEditor;

[CustomEditor(typeof(AugmentationData))]
public class AugmentationDataEditor : Editor
{
    // SerializedProperty for stats and augmentStats
    private SerializedProperty statsProperty;
    private SerializedProperty augmentStatsProperty;

    private void OnEnable()
    {
        // Find the serialized properties
        statsProperty = serializedObject.FindProperty("stats");
        augmentStatsProperty = serializedObject.FindProperty("augmentStats");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Draw the stats property
        EditorGUILayout.PropertyField(statsProperty);

        // If stats is true, draw augmentStats
        if (statsProperty.boolValue)
        {
            EditorGUILayout.PropertyField(augmentStatsProperty);
        }

        // Apply changes to the serializedObject
        serializedObject.ApplyModifiedProperties();
    }
}
