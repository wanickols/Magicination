
using Battle;
using System;

namespace Core
{
    //Hold all equipables for a character
    public class Equipment
    {

        public Action changedEquipment;

        private Equippable weapon;
        private Equippable head;
        private Equippable arms;
        private Equippable chest;
        private Equippable legs;
        private Equippable accessoryOne;


        public void Equip(Equippable item)
        {
            switch (item.Type)
            {
                case EquippableType.Weapon:
                    exchangeWeapon(item);
                    break;
                case EquippableType.Head:
                    exchangeHead(item);
                    break;
                case EquippableType.Arms:
                    exchangeArms(item);
                    break;
                case EquippableType.Chest:
                    exchangeChest(item);
                    break;
                case EquippableType.Legs:
                    exchangeLegs(item);
                    break;
                case EquippableType.Accesesory:
                    exchangeAccessory(item);
                    break;
                default:
                    break;
            };

            changedEquipment?.Invoke();
        }

        private void Equip(EquippableType type)
        {
            switch (type)
            {
                case EquippableType.Weapon:
                    exchangeWeapon(null);
                    break;
                case EquippableType.Head:
                    exchangeHead(null);
                    break;
                case EquippableType.Arms:
                    exchangeArms(null);
                    break;
                case EquippableType.Chest:
                    exchangeChest(null);
                    break;
                case EquippableType.Legs:
                    exchangeLegs(null);
                    break;
                case EquippableType.Accesesory:
                    exchangeAccessory(null);
                    break;
                default:
                    break;
            };

            changedEquipment?.Invoke();
        }

        public Equippable getEquipped(EquippableType type)
        {
            Equippable equippable = type switch
            {
                EquippableType.Weapon => weapon,
                EquippableType.Head => head,
                EquippableType.Arms => arms,
                EquippableType.Chest => chest,
                EquippableType.Legs => legs,
                EquippableType.Accesesory => accessoryOne,
                _ => null
            };

            return equippable;
        }

        public Stats getEquipmentTotalStats()
        {
            // Initialize an empty Stats object
            Stats total = new Stats();
            // Add each equipment's Stats to the total, if not null
            total += weapon?.Stats;
            total += head?.Stats;
            total += chest?.Stats;
            total += legs?.Stats;
            total += arms?.Stats;
            total += accessoryOne?.Stats;
            // Return the total Stats
            return total;
        }
        public void Remove(EquippableType type) => Equip(type);
        ///Private

        private void exchangeWeapon(Equippable item)
        {
            Party.arsenal.Add(weapon);
            weapon = item;
            Party.arsenal.Remove(item);
        }

        private void exchangeHead(Equippable item)
        {
            Party.arsenal.Add(head);
            head = item;
            Party.arsenal.Remove(item);
        }
        private void exchangeArms(Equippable item)
        {
            Party.arsenal.Add(arms);
            arms = item;
            Party.arsenal.Remove(item);
        }

        private void exchangeChest(Equippable item)
        {
            Party.arsenal.Add(chest);
            chest = item;
            Party.arsenal.Remove(item);
        }
        private void exchangeLegs(Equippable item)
        {
            Party.arsenal.Add(legs);
            legs = item;
            Party.arsenal.Remove(item);
        }

        private void exchangeAccessory(Equippable item)
        {
            Party.arsenal.Add(accessoryOne);
            accessoryOne = item;
            Party.arsenal.Remove(item);
        }
    }

}