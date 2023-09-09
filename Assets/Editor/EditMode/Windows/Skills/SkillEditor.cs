using MGCNTN;
using UnityEditor;
using UnityEngine;

public class SkillEditor : MonoBehaviour
{
    public class SkillEditorWindow : EditorWindow
    {
        public delegate void SkillCreatedEventHandler(Skill newSkill);
        public static event SkillCreatedEventHandler OnSkillCreated;

        private string displayName = "New Skill";
        private string description = "";
        private int requiredLevel = 1;
        private Sprite sprite;
        private AugmentationData augData;

        [MenuItem("Window/Skill Editor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(SkillEditorWindow), false, "Skill Editor");
        }

        private void OnGUI()
        {
            displayName = EditorGUILayout.TextField("Display Name", displayName);
            description = EditorGUILayout.TextField("Description", description);
            requiredLevel = EditorGUILayout.IntField("Required Level", requiredLevel);
            sprite = EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false) as Sprite;
            augData = EditorGUILayout.ObjectField("Augmentation Data", augData, typeof(AugmentationData), false) as AugmentationData;

            if (GUILayout.Button("Create"))
            {
                Skill newSkill = CreateNewSkill();
                Close();
            }
        }

        private Skill CreateNewSkill()
        {
            SkillData newSkillData = new SkillData();
            newSkillData.displayName = displayName;
            newSkillData.description = description;
            newSkillData.requiredLevel = requiredLevel;
            newSkillData.sprite = sprite;
            newSkillData.augData = augData;

            Skill newSkill = ScriptableObject.CreateInstance<Skill>();
            newSkill.Data = newSkillData;
            // Save the new skill as an asset (optional).
            AssetDatabase.CreateAsset(newSkill, $"Assets/Resources/Skills/{displayName}.asset");
            AssetDatabase.SaveAssets();

            return newSkill;
        }


    }
}
