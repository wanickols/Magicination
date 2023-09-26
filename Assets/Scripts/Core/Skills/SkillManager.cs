#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class SkillManager : MonoSavable
    {

        ///Private Variables
        private List<Skill> skills = new List<Skill>();

        ///Protected Variable
        protected override string customPath { get => "Creation/Skills.json"; }

        protected override string errorMessage { get => "Error in Skill Manager Saving and Loading"; }

        private void Awake() { SaveSkillsInFolder("Assets/Objects/Skills"); }

        ///Public Functions
        //Accessors
        public Skill getSkillByName(string name) => skills.Find(skill => skill.Data.displayName == name);
        public Skill getSkillByInt(int index) => skills[index];
        public void Add(Skill skill) => skills.Add(skill);

        //Editor
        public List<GUIContent> getSkillGUIs()
        {
            List<GUIContent> skillContent = new List<GUIContent>();


            foreach (var skill in skills)
                skillContent.Add(new GUIContent(skill.Data.displayName));

            return skillContent;
        }


        //Parent Implementations
        public override bool LoadData()
        {
            string[] jsons = loadFromFile();

            if (jsons == null)
                return false;

            skills.Clear();

            foreach (string json in jsons)
                skills.Add(JsonToSkill(json));


            return true;
        }

        public override bool SaveData()
        {
            List<string> jsons = new List<string>();

            foreach (Skill skill in skills)
                jsons.Add(JsonUtility.ToJson(skill.Data));


            if (jsons.Count <= 0)
                return false;

            saveToFile(jsons);

            return true;
        }


        ///Private Functions
        private Skill JsonToSkill(String json)
        {
            Skill skill = new Skill();
            SkillData data = JsonUtility.FromJson<SkillData>(json);
            skill.Data = data;
            //Do image stuff here
            return skill;
        }

        //Editor
#if UNITY_EDITOR
        public void SaveSkillsInFolder(string folderPath)
        {
            skills.Clear();

            string[] guids = AssetDatabase.FindAssets("t:Skill", new[] { folderPath }); // Adjust the folder path accordingly
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Skill skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
                skills.Add(skill);
            }

            SaveData();
        }
#endif
    }
}