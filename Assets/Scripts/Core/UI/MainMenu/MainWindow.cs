using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {

        /// Private Paremeters
        [SerializeField] private GameObject EquipWindow, arsenalWindow, addableStatsList;

        private EquipmentMenu equipMenu;

        private EquippableType lastSelectedEquippableType = EquippableType.Weapon;


        /// Unity Functions
        void Start()
        {
            ShowDefaultView();
            equipMenu = EquipWindow.GetComponent<EquipmentMenu>();
        }


        /// Public
        public void ShowDefaultView()
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }

        //Equip
        public void ShowEquipmentView(int selected)
        {


            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(false);
            }

            EquipWindow.SetActive(true);

            PartyMember selectedMember = new PartyMember();


            selectedMember = Party.ActiveMembers[selected];

            //Show Equipment
            equipMenu.initValues(selectedMember);
        }

        public void ShowEquippableSelection(Selector equippableSelector, int selected)
        {

            arsenalWindow.SetActive(true);

            lastSelectedEquippableType = (EquippableType)selected;

            //Get list from arsenal
            List<Equippable> equippables = Party.arsenal.getEquippables(lastSelectedEquippableType);

            int count = equippables.Count;

            for (int i = 0; i < 16; i++)
            {
                EquippableOption option = equippableSelector.getChild(i).GetComponent<EquippableOption>();

                if (i < count)
                    option.changeOption(equippables[i]);
            }

            updateStats(equippableSelector);
        }

        public void hideEquippableSection()
        {
            arsenalWindow.SetActive(false);
        }

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
            {
                equipment.Remove(lastSelectedEquippableType);
            }

            updateEquipMenu();
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

        /// Private
        private void updateEquipMenu() => equipMenu.updateValues();
    }
}
