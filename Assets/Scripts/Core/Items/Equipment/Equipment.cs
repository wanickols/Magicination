
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
        }

        public Equippable getEquipped(EquipmentType type)
        {
            Equippable equippable = type switch
            {
                EquipmentType.Weapon => weapon,
                EquipmentType.Head => head,
                EquipmentType.Arms => arms,
                EquipmentType.Chest => chest,
                EquipmentType.Legs => legs,
                EquipmentType.Accesesory => accessoryOne,
                _ => new Equippable()
            };

            if (equippable == null)
            {
                equippable = new Equippable();
                equippable.name = "Empty";
            }

            return equippable;
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