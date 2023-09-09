using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SkillEditor;

namespace MGCNTN.Core
{
    [CustomEditor(typeof(SkillManager))]
    public class SkillManagerEditor : Editor
    {

        private SkillManager manager;
        private Skill option1;
        private Skill option2;
        private Skill newSkill;

        private List<GUIContent> skillGUIContents;
        private List<Skill> skillList;

        private void OnEnable()
        {
            manager = (SkillManager)target;
            RetrieveSkillList();
            SkillEditorWindow.OnSkillCreated += HandleSkillCreated;
        }

        private void RetrieveSkillList()
        {
            string[] guids = AssetDatabase.FindAssets("t:Skill", new[] { "Assets/Skills" }); // Adjust the folder path accordingly
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Skill skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
                skillList.Add(skill);
                GUIContent guiContent = new GUIContent(skill.Data.displayName);
                skillGUIContents.Add(guiContent);
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            option1 = ShowSkillSelectionPopup();
            option2 = ShowSkillSelectionPopup();

            // Button to create a new skill
            if (GUILayout.Button("Create New Skill"))
                CreateSkillPrompt();
        }

        private Skill ShowSkillSelectionPopup(string dropdownTitle = "Skill")
        {
            int selectedSkillIndex = -1;

            if (GUILayout.Button(dropdownTitle))
            {
                // Display the custom menu and store the selected index.
                EditorUtility.DisplayCustomMenu(
                   new Rect(Event.current.mousePosition, Vector2.zero),
                   skillGUIContents.ToArray(),
                   selectedSkillIndex,
                   SkillSelectedCallback,
                   null // Adjust the width as needed.
               );
            }

            if (selectedSkillIndex >= 0)
            {
                // Store the selected skill in your custom variable (option1 or option2).
                return skillList[selectedSkillIndex];
            }

            // Handle the case where no skill was selected.
            return null;
        }

        private void CreateSkillPrompt()
        {
            SkillEditorWindow skillEditorWindow = new SkillEditorWindow();
            skillEditorWindow.Show();
        }

        private void SkillSelectedCallback(object userData, string[] options, int selected)
        {
            // Handle the selected skill here.
        }
        private void HandleSkillCreated(Skill newSkill)
        {
            this.newSkill = newSkill;
            // Add the new skill to your skillList and skillGUIContents.
            skillList.Add(newSkill);
            // Add a corresponding GUIContent to skillGUIContents.
            GUIContent skillContent = new GUIContent(newSkill.Data.displayName);
            skillGUIContents.Add(skillContent);

            // Repaint the editor window or perform any necessary updates.
            Repaint();
        }

        private void OnDisable() => SkillEditorWindow.OnSkillCreated -= HandleSkillCreated;

    }
}
