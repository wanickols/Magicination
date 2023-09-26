using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MGCNTN.Core
{
    [Serializable]
    public class CombinationData
    {
        public string parentSkill1;
        public string parentSkill2;
        public string resultSkill;
    }

    public class CombinationManager : MonoSavable
    {
        //
        private static Dictionary<Tuple<string, string>, string> combinationDictionary = new Dictionary<Tuple<string, string>, string>();
        private string currError = string.Empty;
        private SkillManager skillManager;

        protected override string errorMessage { get => currError; }
        protected override string customPath { get => "Creation/Combinations.json"; }

        ///Public Functions
        public void init(SkillManager manager) => skillManager = manager;
        public Skill FindCombination(string skill1, string skill2)
        {
            Tuple<string, string> key = new Tuple<string, string>(skill1, skill2);

            if (combinationDictionary.TryGetValue(key, out string result))
                return skillManager.getSkillByName(result);

            // Combination not found
            return null;
        }

        public void AddCombination(string skill1, string skill2, string resultSkill)
        {
            Tuple<string, string> key = new Tuple<string, string>(skill1, skill2);
            combinationDictionary[key] = resultSkill;
        }

        public void AddCombination(CombinationData combination)
        {
            Tuple<string, string> key = new Tuple<string, string>(combination.parentSkill1, combination.parentSkill2);
            combinationDictionary[key] = combination.resultSkill;
        }

        ///Private
        public override bool LoadData()
        {
            string[] jsons = loadFromFile();

            if (jsons == null)
                return false;

            combinationDictionary.Clear();

            foreach (string json in jsons)
                loadCombinationFromJson(json);

            return true;
        }

        //
        public override bool SaveData()
        {
            // Collect combination data from combinationDictionary
            List<CombinationData> combinationDataList = new List<CombinationData>();

            // Iterate through combinationDictionary and add data to the list
            foreach (var key in combinationDictionary.Keys)
            {
                CombinationData data = new CombinationData();
                data.parentSkill1 = key.Item1;
                data.parentSkill2 = key.Item2;
                data.resultSkill = combinationDictionary[key];
                combinationDataList.Add(data);
            }

            if (combinationDataList.Count <= 0)
                return true;

            List<string> jsons = new List<string>();
            jsons.Capacity = (combinationDataList.Count * 3) + 2;
            // Serialize the combination data to JSON format
            foreach (CombinationData combinationData in combinationDataList)
                jsons.Add(EditorJsonUtility.ToJson(combinationData));

            // Write the JSON data to "Combinations.json"
            saveToFile(jsons);
            return true;
        }

        private void loadCombinationFromJson(string json)
        {
            CombinationData combinationData = JsonUtility.FromJson<CombinationData>(json);

            Skill parentSkill1 = skillManager.getSkillByName(combinationData.parentSkill1);
            Skill parentSkill2 = skillManager.getSkillByName(combinationData.parentSkill2);
            Skill resultSkill = skillManager.getSkillByName(combinationData.resultSkill);

            if (parentSkill1 != null && parentSkill2 != null && resultSkill != null)
                AddCombination(combinationData);
            else
                currError = "Failed to get skill loaded";
        }
    }
}