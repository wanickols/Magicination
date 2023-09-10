using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MGCNTN.Core
{
    [Serializable]
    public class CombinationData
    {
        /*public CombinationData(string skill1, string skill2, string comboSkill)
        {
            this.parentSkill1 = skill1;
            this.parentSkill2 = skill2;
            this.resultSkill = comboSkill;
        }*/
        public string parentSkill1;
        public string parentSkill2;
        public string resultSkill;
    }

    public class SkillManager : MonoBehaviour
    {

        private Dictionary<Tuple<Skill, Skill>, Skill> combinationDictionary = new Dictionary<Tuple<Skill, Skill>, Skill>();

        private void Awake() => LoadCombinations();

        public Skill FindCombination(Skill skill1, Skill skill2)
        {
            Tuple<Skill, Skill> key = new Tuple<Skill, Skill>(skill1, skill2);

            if (combinationDictionary.TryGetValue(key, out Skill result))
                return result;


            // Combination not found
            return null;
        }

        public void AddCombination(Skill skill1, Skill skill2, Skill resultSkill)
        {
            Tuple<Skill, Skill> key = new Tuple<Skill, Skill>(skill1, skill2);
            combinationDictionary[key] = resultSkill;
            SaveCombinations();
        }

        ///Private
        private void LoadCombinations()
        {
            // Load the JSON data from "Combinations.json"
            string json = File.ReadAllText("Assets/Resources/Combinations.json");

            // Deserialize the JSON data into combination data
            List<CombinationData> combinationDataList = JsonUtility.FromJson<List<CombinationData>>(json);

            // Iterate through combination data and create combinations
            foreach (CombinationData combinationData in combinationDataList)
            {
                Skill parentSkill1 = Resources.Load<Skill>("Skills/" + combinationData.parentSkill1);
                Skill parentSkill2 = Resources.Load<Skill>("Skills/" + combinationData.parentSkill2);
                Skill resultSkill = Resources.Load<Skill>("Skills/" + combinationData.resultSkill);

                if (parentSkill1 != null && parentSkill2 != null && resultSkill != null)
                {
                    AddCombination(parentSkill1, parentSkill2, resultSkill);
                }
            }
        }
        private void SaveCombinations()
        {

            LoadCombinations();

            // Collect combination data from combinationDictionary
            List<CombinationData> combinationDataList = new List<CombinationData>();

            // Iterate through combinationDictionary and add data to the list

            foreach (var key in combinationDictionary.Keys)
            {
                CombinationData data = new CombinationData();
                data.parentSkill1 = key.Item1.Data.displayName;
                data.parentSkill2 = key.Item2.Data.displayName;
                data.resultSkill = combinationDictionary[key].Data.displayName;
                combinationDataList.Add(data);
            }

            if (combinationDataList.Count <= 0)
                return;

            List<string> jsons = new List<string>();
            jsons.Capacity = (combinationDataList.Count * 3) + 2;
            jsons.Add("{");
            // Serialize the combination data to JSON format
            foreach (CombinationData combinationData in combinationDataList)
                jsons.Add(EditorJsonUtility.ToJson(combinationData));


            jsons.Add("}");


            // Write the JSON data to "Combinations.json"
            File.WriteAllLines("Assets/Resources/Combinations.json", jsons);
        }

    }
}