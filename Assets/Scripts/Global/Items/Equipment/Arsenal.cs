using System;
using System.Collections.Generic;
using UnityEditor;

namespace MGCNTN
{
    /// <summary>
    /// This is going to hold Equipment, not all ITEMS!! This will be used to store equipment 
    /// </summary>
    public class Arsenal : Core.Savable
    {
        private const int MAX_COUNT_OF_EQUIPMENT_TYPE = 16;
        private List<Equippable> equippableList = new List<Equippable>();

        private Dictionary<EquippableType, int> typeCount = new Dictionary<EquippableType, int>();


        protected override string customPath { get => "Playtime/Arsenal.json"; }

        protected override string errorMessage { get => "Error in Arsenal Saving and Loading"; }

        public Arsenal() : base()
        {
            foreach (EquippableType type in Enum.GetValues(typeof(EquippableType)))
                typeCount.Add(type, 0);
        }

        public bool Add(Equippable equippable)
        {
            if (equippable == null || !checkCount(equippable.Type))
                return false;

            ++typeCount[equippable.Type];
            equippableList.Add(equippable);


            UnityEngine.Debug.Log("Arsenal Count: " + equippableList.Count);
            return true;

        }

        public void Remove(Equippable equippable)
        {
            if (equippable == null) return;

            --typeCount[equippable.Type];
            equippableList.Remove(equippable);
        }

        public void Clear() { equippableList.Clear(); typeCount.Clear(); }

        public List<Equippable> getEquippables(EquippableType type)
        {
            List<Equippable> equippables = new List<Equippable>();

            foreach (Equippable e in equippableList)
            {
                if (type == e.Type)
                    equippables.Add(e);
            }

            return equippables;
        }

        public List<Equippable> getEquippables() => equippableList;


        /// Private
        private bool checkCount(EquippableType type) => typeCount[type] < MAX_COUNT_OF_EQUIPMENT_TYPE;

        public override bool SaveData()
        {
            List<string> jsons = new List<string>();

            foreach (Equippable e in equippableList)
            {
                string path = AssetDatabase.GetAssetPath(e);
                jsons.Add(path);
            }


            if (jsons.Count <= 0)
                return false;

            saveToFile(jsons);

            return true;
        }

        public override bool LoadData()
        {
            string[] jsons = loadFromFile();

            if (jsons == null || jsons.Length <= 0)
                return false;

            foreach (string json in jsons)
            {
                Equippable newEquipable = AssetDatabase.LoadAssetAtPath<Equippable>(json);
                Add(newEquipable);
            }

            return true;
        }
    }
}