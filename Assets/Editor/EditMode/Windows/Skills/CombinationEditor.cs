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
        List<Skill> selectedSkills = new List<Skill>();

        private List<GUIContent> skillGUIContents = new List<GUIContent>();
        private List<Skill> skillList = new List<Skill>();
        private int currOption = 0;

        private void OnEnable()
        {
            selectedSkills.Clear();
            selectedSkills.Add(new Skill());
            selectedSkills.Add(new Skill());
            selectedSkills.Add(new Skill());

            manager = (SkillManager)target;
            manager.LoadCombinations();
            RetrieveSkillList();
            SkillEditorWindow.OnSkillCreated += HandleSkillCreated;
        }

        private void RetrieveSkillList()
        {
            string[] guids = AssetDatabase.FindAssets("t:Skill", new[] { "Assets/Resources/Skills" }); // Adjust the folder path accordingly
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


            ShowSkillSelectionPopup(selectedSkills[0]?.Data.displayName, 0);
            ShowSkillSelectionPopup(selectedSkills[1]?.Data.displayName, 1);
            ShowSkillSelectionPopup(selectedSkills[2]?.Data.displayName, 2);



            // Button to create a new skill
            if (GUILayout.Button("Create New Skill"))
                CreateSkillPrompt();

            // Button to create a new skill
            if (selectedSkills[0] && selectedSkills[1] && selectedSkills[2] && GUILayout.Button("Combine"))
            {
                manager.AddCombination(selectedSkills[0], selectedSkills[1], selectedSkills[2]);
                manager.SaveCombinations();
            }
            else
            {
                GUI.enabled = false; // Disable the button
                GUILayout.Button("Combine"); // Display the greyed out button
                GUI.enabled = true; // Enable GUI elements again
            }
        }

        private void ShowSkillSelectionPopup(string dropdownTitle = "Skill", int option = 0)
        {
            int selectedSkillIndex = -1;

            if (skillGUIContents == null)
                return;

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

                currOption = option;
            }
        }

        private void SkillSelectedCallback(object userData, string[] options, int selected)
        {
            // Store the selected skill index.
            if (selected >= 0)
            {
                Skill selectedSkill = skillList[selected];
                Debug.Log("Selected Skill: " + selectedSkill.Data.displayName);
                Debug.Log("Current Option: " + currOption);
                selectedSkills[currOption] = selectedSkill;
            }

            // Log the updated selectedSkillIndex.
            Debug.Log(options[selected]);
        }

        private void CreateSkillPrompt()
        {
            SkillEditorWindow skillEditorWindow = new SkillEditorWindow();
            skillEditorWindow.Show();
        }


        private void HandleSkillCreated(Skill newSkill)
        {
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
