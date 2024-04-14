using System;
using System.Collections.Generic;
using UnityEditor;

namespace MGCNTN
{
    //Hold all equipables for a character
    public class Equipment
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

        public Arsenal arsenal;

        public void Equip(Equippable item)
        {
            if (item == null) return;

            exchangeItem(item, item.Type);

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
                arsenal.Add(equipmentSlots[type]);
                equipmentSlots[type] = item;

            }
            else
            {

                arsenal.Add(equipmentSlots[item.Type]);
                equipmentSlots[item.Type] = item;
                arsenal.Remove(item);
            }
        }

        public List<string> getSaveList()
        {
            List<string> paths = new List<string>();

            foreach (Equippable equipable in equipmentSlots.Values)
            {
                // Get the path to the object using AssetDatabase
                string path = AssetDatabase.GetAssetPath(equipable);

                // Add the path to the list of paths
                paths.Add(path);
            }

            return paths;
        }

        public void loadEquipmentFromList(List<string> paths)
        {
            foreach (string path in paths)
            {
                // Load the ScriptableObject Equippable from the path
                Equippable newEquipable = AssetDatabase.LoadAssetAtPath<Equippable>(path);

                // Call the Equip function passing in the new item
                Equip(newEquipable);
            }
        }

    }

}