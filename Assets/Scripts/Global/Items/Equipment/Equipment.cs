using MGCNTN.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    //Hold all equipables for a character
    public class Equipment : Savable
    {

        public Action changedEquipment;

        private Dictionary<EquippableType, Equippable> equipmentSlots = new Dictionary<EquippableType, Equippable>
        {
        { EquippableType.Weapon, null },
        { EquippableType.Head, null },
        { EquippableType.Arms, null },
        { EquippableType.Chest, null },
        { EquippableType.Legs, null },
        { EquippableType.Accesesory, null }
        };

        private string currPath = "Equipment.json";
        protected override string customPath => currPath;

        protected override string errorMessage { get => "Error in Equipment Saving and Loading"; }

        public void Equip(Equippable item)
        {
            exchangeItem(item);

            changedEquipment?.Invoke();
        }

        private void Unequip(EquippableType type)
        {
            exchangeItem(null, type);
            changedEquipment?.Invoke();
        }

        public Equippable getEquipped(EquippableType type) => equipmentSlots[type];
        public Stats getEquipmentTotalStats()
        {
            // Initialize an empty Stats object
            Stats total = new Stats();

            foreach (Equippable equipable in equipmentSlots.Values)
            {
                if (equipable)
                    total += equipable.Stats!;
            }
            return total;
        }
        public void Remove(EquippableType type) => Unequip(type);
        ///Private

        private void exchangeItem(Equippable item, EquippableType type = EquippableType.Weapon)
        {
            if (!item)
            {
                Party.arsenal.Add(equipmentSlots[type]);
                equipmentSlots[type] = item;

            }
            else
            {

                Party.arsenal.Add(equipmentSlots[item.Type]);
                equipmentSlots[item.Type] = item;
                Party.arsenal.Remove(item);
            }
        }

        public void Save(string path)
        {
            currPath = path + "Equipment.json";
            SaveData();
        }

        public override bool SaveData()
        {
            List<string> jsons = new List<string>();

            foreach (Equippable equipable in equipmentSlots.Values)
                jsons.Add(JsonUtility.ToJson(equipable));

            saveToFile(jsons);

            return true;

        }

        public override bool LoadData()
        {
            string[] jsons = loadFromFile();

            if (jsons == null)
                return false;


            foreach (string json in jsons)
                Equip(JsonUtility.FromJson<Equippable>(json));


            return true;
        }
    }

}