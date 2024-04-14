using MGCNTN.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MGCNTN
{
    public class ItemBag : Savable
    {
        public Dictionary<Consumable, int> consumables { get; private set; } = new Dictionary<Consumable, int>();

        protected override string customPath { get => "Playtime/Itembag.json"; }

        protected override string errorMessage { get => "Error in ItemBag Saving and Loading"; }


        /// Public Functions
        public void Add(Consumable consumable)
        {

            if (consumables.ContainsKey(consumable))
                consumables[consumable] += consumable.Data.quantity;
            else
                consumables.Add(consumable, 1);

        }

        public void Remove(Consumable consumable)
        {
            if (!consumables.ContainsKey(consumable))
            {
                Debug.Log($"Consumable Item Not Found to Consume: {consumable}");
                return;
            }

            consumables[consumable] -= consumable.Data.quantity;

            if (consumables[consumable] <= 0)
                consumables.Remove(consumable);

        }

        public void clear() => consumables.Clear();


        //Save and Load
        public override bool SaveData()
        {
            List<string> jsons = new List<string>();

            //foreach pair of consumable, int, save to json string 
            foreach (KeyValuePair<Consumable, int> con in consumables)
            {

                jsons.Add(con.Value.ToString());
                string path = AssetDatabase.GetAssetPath(con.Key);
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

            for (int i = 0; i < jsons.Length; i += 2)
            {
                //Load int first
                int count = int.Parse(jsons[i]);

                Consumable consumable = AssetDatabase.LoadAssetAtPath<Consumable>(jsons[i + 1]);

                consumables.Add(consumable, count);
            }

            return true;
        }
    }
}