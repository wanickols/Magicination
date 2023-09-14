using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MGCNTN.Core
{
    [CustomEditor(typeof(CombinationManager))]
    public class SkillManagerEditor : Editor
    {

        private CombinationManager combinationManager;
        private SkillManager skillManager;
        List<string> selectedSkills = new List<string>();

        private List<GUIContent> skillGUIContents = new List<GUIContent>();
        private int currOption = 0;

        private void OnEnable()
        {
            selectedSkills.Clear();
            selectedSkills.Add(string.Empty);
            selectedSkills.Add(string.Empty);
            selectedSkills.Add(string.Empty);

            //Load Combinations
            combinationManager = (CombinationManager)target;

            skillManager = combinationManager.transform.GetComponent<SkillManager>();

            combinationManager.init(skillManager);
            combinationManager.LoadData();


            skillGUIContents = skillManager.getSkillGUIs();

            SkillEditorWindow.OnSkillCreated += HandleSkillCreated;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();


            ShowSkillSelectionPopup(selectedSkills[0], 0);
            ShowSkillSelectionPopup(selectedSkills[1], 1);
            ShowSkillSelectionPopup(selectedSkills[2], 2);


            // Button to create a combination
            if (selectedSkills[0] != string.Empty && selectedSkills[1] != string.Empty && selectedSkills[2] != string.Empty)
            {
                if (GUILayout.Button("Combine"))
                {
                    combinationManager.AddCombination(selectedSkills[0], selectedSkills[1], selectedSkills[2]);
                    combinationManager.SaveData();
                }
            }
            else
            {
                GUI.enabled = false; // Disable the button
                GUILayout.Button("Combine"); // Display the greyed out button with a label
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
                Skill selectedSkill = skillManager.getSkillByInt(selected);
                Debug.Log("Selected Skill: " + selectedSkill.Data.displayName);
                Debug.Log("Current Option: " + currOption);
                selectedSkills[currOption] = selectedSkill.Data.displayName;
            }

            // Log the updated selectedSkillIndex.
            Debug.Log(options[selected]);
        }


        private void HandleSkillCreated(Skill newSkill)
        {
            // Add a corresponding GUIContent to skillGUIContents.
            GUIContent skillContent = new GUIContent(newSkill.Data.displayName);
            skillGUIContents.Add(skillContent);

            // Repaint the editor window or perform any necessary updates.
            Repaint();
        }

        private void OnDisable() => SkillEditorWindow.OnSkillCreated -= HandleSkillCreated;

    }
}
