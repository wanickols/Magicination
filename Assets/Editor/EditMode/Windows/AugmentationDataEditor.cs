using MGCNTN;
using UnityEditor;

[CustomEditor(typeof(AugmentationData))]
public class AugmentationDataEditor : Editor
{
    // SerializedProperty for stats and augmentStats
    private SerializedProperty statsProperty;
    private SerializedProperty augmentStatsProperty;

    private SerializedProperty statusProperty;
    private SerializedProperty statusListProperty;

    private void OnEnable()
    {
        // Find the serialized properties
        statsProperty = serializedObject.FindProperty("hasStats");
        augmentStatsProperty = serializedObject.FindProperty("augmentStats");

        statusProperty = serializedObject.FindProperty("hasStatus");
        statusListProperty = serializedObject.FindProperty("statusList");
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

        EditorGUILayout.PropertyField(statusProperty);

        // If status is true, draw statusList
        if (statsProperty.boolValue)
        {
            EditorGUILayout.PropertyField(statusListProperty);
        }



        // Apply changes to the serializedObject
        serializedObject.ApplyModifiedProperties();
    }
}
