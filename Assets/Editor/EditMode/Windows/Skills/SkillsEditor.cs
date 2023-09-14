using UnityEditor;
using UnityEngine;

namespace MGCNTN.Core
{
    [CustomEditor(typeof(SkillManager))]
    public class SkillsEditor : Editor
    {
        private SkillManager skillManager;

        ///Unity Functions
        private void OnEnable()
        {
            skillManager = (SkillManager)target;
            skillManager.SaveSkillsInFolder("Assets/Objects/Skills");
            skillManager.LoadData();
            SkillEditorWindow.OnSkillCreated += HandleSkillCreated;
        }

        ///Editor Functions
        public override void OnInspectorGUI()
        {
            // Button to create a new skill
            if (GUILayout.Button("Create New Skill"))
                CreateSkillPrompt();
        }


        ///Private Functions
        private void CreateSkillPrompt()
        {
            SkillEditorWindow skillEditorWindow = new SkillEditorWindow();
            skillEditorWindow.Show();
        }

        private void HandleSkillCreated(Skill newSkill)
        {
            skillManager.Add(newSkill);
        }

        private void OnDisable() => SkillEditorWindow.OnSkillCreated -= HandleSkillCreated;

    }
}