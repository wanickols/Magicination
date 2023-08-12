
namespace Core
{
    //Hold all equipables for a character
    public class Equipment
    {

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
                case EquipmentType.Weapon:
                    exchangeWeapon(item);
                    break;
                case EquipmentType.Head:
                    exchangeHead(item);
                    break;
                case EquipmentType.Arms:
                    exchangeArms(item);
                    break;
                case EquipmentType.Chest:
                    exchangeChest(item);
                    break;
                case EquipmentType.Legs:
                    exchangeLegs(item);
                    break;
                case EquipmentType.Accesesory:
                    exchangeAccessory(item);
                    break;
                default:
                    break;
            };

            //Recalculate Stats
        }

        private void exchangeWeapon(Equippable item)
        {
            //Remove item from inventory
            //Readd current weapon to inventory

            weapon = item;
        }

        private void exchangeHead(Equippable item)
        {
            head = item;
        }
        private void exchangeArms(Equippable item)
        {
            arms = item;
        }

        private void exchangeChest(Equippable item)
        {
            chest = item;
        }
        private void exchangeLegs(Equippable item)
        {
            legs = item;
        }

        private void exchangeAccessory(Equippable item)
        {
            accessoryOne = item;
        }
    }

}