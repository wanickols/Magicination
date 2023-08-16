using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core
{
    public class ArsenalMenu : MonoBehaviour
    {

        [HideInInspector] public EquippableType lastSelectedEquippableType = EquippableType.Weapon;
        [SerializeField] private GameObject addableStatsList;

        private EquipMenu equipMenu;


        public void init(EquipMenu equipMenu) => this.equipMenu = equipMenu;

        public void swapEquippable(Selector equippableSelector)
        {
            Equipment equipment = equipMenu.partyMember.equipment;

            EquippableOption option = equippableSelector.selectedTransform.GetComponent<EquippableOption>();
            Equippable equippable = null;

            if (option.equippable != null)
                equippable = option.equippable;

            option.changeOption(equipment.getEquipped(lastSelectedEquippableType));
            clearEquippables(equippableSelector);

            if (equippable != null)
                equipment.Equip(equippable);
            else
                equipment.Remove(lastSelectedEquippableType);


            equipMenu.updateValues();
        }

        public void updateStats(Selector selector)
        {
            // Get Stuff
            EquippableOption option = selector.selectedTransform.GetComponent<EquippableOption>();
            Equippable equipped = equipMenu.partyMember.equipment.getEquipped(lastSelectedEquippableType);

            //Init Stat Comparison Lists
            List<int> displayStats, equippedStats;
            displayStats = equippedStats = new List<int> { 0, 0, 0, 0, 0, 0 };

            //Null Cheks
            //Selected
            if (option.equippable != null)
                displayStats = option.equippable.Stats.getDisplayStatValues();

            //Equipped
            if (equipped != null)
                equippedStats = equipped.Stats.getDisplayStatValues();

            //Set Stats
            loopStats(displayStats, equippedStats);
        }

        private void loopStats(List<int> displayStats, List<int> equippedStats)
        {
            int i = 0;
            foreach (Transform stat in addableStatsList.transform)
            {
                stat.gameObject.SetActive(true);
                TextMeshProUGUI textBox = stat.GetComponent<TextMeshProUGUI>();
                setTextFromStat(textBox, displayStats[i], equippedStats[i]);
                i++;
            }
        }

        private void setTextFromStat(TextMeshProUGUI textBox, int value, int currValue)
        {
            //Change Stats
            if (value == currValue)
                textBox.text = string.Empty;
            else
            {
                if (value < currValue)
                {
                    textBox.text = $"- {currValue - value}";
                    textBox.color = Color.red;
                }
                else
                {
                    textBox.text = $"+ {currValue + value}";
                    textBox.color = Color.cyan;
                }
            }
        }

        public void clearEquippables(Selector selector)
        {
            for (int i = 0; i < 16; i++)
            {
                EquippableOption option = selector.getChild(i).GetComponent<EquippableOption>();
                option.clear();
            }
        }
    }
}
