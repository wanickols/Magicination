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
        public string parentSkill1;
        public string parentSkill2;
        public string resultSkill;
    }

    public class SkillManager : MonoBehaviour
    {

        private static Dictionary<Tuple<Skill, Skill>, Skill> combinationDictionary = new Dictionary<Tuple<Skill, Skill>, Skill>();

        private void Awake() => LoadCombinations();

        public Skill FindCombination(Skill skill1, Skill skill2)
        {
            Tuple<Skill, Skill> key = new Tuple<Skill, Skill>(skill1, skill2);

            if (combinationDictionary.TryGetValue(key, out Skill result))
                return result;

            Tuple<Skill, Skill> key2 = new Tuple<Skill, Skill>(skill2, skill1);

            if (combinationDictionary.TryGetValue(key2, out Skill result2))
                return result2;

            // Combination not found
            return null;
        }

        public void AddCombination(Skill skill1, Skill skill2, Skill resultSkill)
        {
            Tuple<Skill, Skill> key = new Tuple<Skill, Skill>(skill1, skill2);
            combinationDictionary[key] = resultSkill;
        }

        ///Private
        public void LoadCombinations()
        {
            // Load the JSON data from "Combinations.json"
            string[] jsons = File.ReadAllLines("Assets/Resources/Combinations.json");

            combinationDictionary.Clear();

            foreach (string json in jsons)
            {
                CombinationData combinationData = JsonUtility.FromJson<CombinationData>(json);

                Skill parentSkill1 = Resources.Load<Skill>("Skills/" + combinationData.parentSkill1);
                Skill parentSkill2 = Resources.Load<Skill>("Skills/" + combinationData.parentSkill2);
                Skill resultSkill = Resources.Load<Skill>("Skills/" + combinationData.resultSkill);

                if (parentSkill1 != null && parentSkill2 != null && resultSkill != null)
                    AddCombination(parentSkill1, parentSkill2, resultSkill);

            }



        }
        public void SaveCombinations()
        {
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
            // Serialize the combination data to JSON format
            foreach (CombinationData combinationData in combinationDataList)
                jsons.Add(EditorJsonUtility.ToJson(combinationData));



            // Write the JSON data to "Combinations.json"
            File.WriteAllLines("Assets/Resources/Combinations.json", jsons);
        }

    }
}