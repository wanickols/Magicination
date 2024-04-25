using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class MainWindow : MonoBehaviour
    {


        /// Public Parameters
        [Header("Selectors")]
        public Selector memberSelector;
        public Selector equipmentSelector, equippableSelector;
        public Selector itemActionSelection, itemSelector;
        public Selector skillCategoryBar, skillActionSelector, skillCombinationSelector;
        public TreeSelector skillSelector;
        public Selector partyMemberSelector;

        [Header("Windows")]
        /// Private Paremeters
        [SerializeField] private GameObject EquipWindow, arsenalWindow;
        [SerializeField] private GameObject ItemsWindow, SkillsWindow;
        [SerializeField] private GameObject PartyTargetWindow;


        private EquipMenu equipMenu;
        private ArsenalMenu arsenalMenu;
        private ItemMenu itemMenu;
        private SkillPauseMenu skillMenu;
        private PartyTargetMenu partyTargetMenu;

        public bool canSelectSkill => skillMenu.canSelect;

        /// Unity Functions
        private void Start()
        {
            ShowDefaultView();

            //Equip
            equipMenu = EquipWindow.GetComponent<EquipMenu>();

            //Arsenal
            arsenalMenu = arsenalWindow.GetComponent<ArsenalMenu>();
            arsenalMenu.init(equipMenu);

            //Item
            itemMenu = ItemsWindow.GetComponent<ItemMenu>();

            //Party Targeting
            partyTargetMenu = PartyTargetWindow.GetComponent<PartyTargetMenu>();

            //Skills
            skillMenu = SkillsWindow.GetComponent<SkillPauseMenu>();
        }

        /// Public Functions
        //Main
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



        public void closePartyTargetWindow() => PartyTargetWindow.SetActive(false);
        public void partyTargetSelected(int selected)
        {
            partyTargetMenu.Select(selected);

            switch (partyTargetMenu.selectionType)
            {
                case PartyTargetSelections.item:
                    itemMenu.initItems();
                    break;
                case PartyTargetSelections.skill:
                    break;
            }
        }

        //Equip Menu
        public void ShowEquipmentView(int selected)
        {

            hidePartyMembers();
            EquipWindow.SetActive(true);

            PartyMember selectedMember = Party.ActiveMembers[selected];

            //Show Equipment
            equipMenu.initValues(selectedMember);
        }

        // Arsenal Menu
        public void ShowArsenalView(Selector arsenalSelector, int selected)
        {
            arsenalWindow.SetActive(true);
            arsenalMenu.lastSelectedEquippableType = (EquippableType)selected;

            //Get list from arsenal
            List<Equippable> equippables = Game.manager.party.arsenal.getEquippables((EquippableType)selected);

            int count = equippables.Count;

            for (int i = 0; i < 16; i++)
            {
                EquippableOption option = arsenalSelector.getChild(i).GetComponent<EquippableOption>();

                if (i < count)
                    option.changeOption(equippables[i]);
            }

            arsenalMenu.updateStats(arsenalSelector);
        }
        public void hideEquippableSelection(Selector selector)
        {
            arsenalMenu.clearEquippables(selector);
            arsenalWindow.SetActive(false);
        }
        public void swapEquippable(Selector selector) => arsenalMenu.swapEquippable(selector);
        public void updateEquipmentStats(Selector selector) => arsenalMenu.updateStats(selector);

        ///Item Menu
        public void ShowItemView()
        {
            hidePartyMembers();
            ItemsWindow.SetActive(true);
        }
        public void closeItemView() => ItemsWindow.SetActive(false);
        public bool itemActionSelected(int selected) => itemMenu.setFlag(selected);
        public void itemSelected(int selected)
        {

            Consumable item = itemMenu.selectItem(selected);
            if (!item)
                return;

            item.Consume();
            openPartyTargetWindow(PartyTargetSelections.item, item.Data, null);

        }

        ///Skill Menu
        //Skill View
        public void ShowSkillView(int selected)
        {
            hidePartyMembers();
            SkillsWindow.SetActive(true);

            skillMenu.member = Party.ActiveMembers[selected];
        }
        public void closeSkillView() => SkillsWindow.SetActive(false);

        //Skill Category
        public void ShowSkillTree(int i) => skillMenu.selectTree(i);

        //Skill Selection
        public void skillDescription()
        {

            Debug.Log(skillMenu.showDescription());
        }


        public void skillSelected() => openPartyTargetWindow(PartyTargetSelections.skill, skillMenu.skill.Data, skillMenu.member.Stats);

        //Skill Action
        public void ShowSkillActionWindow() => skillMenu.openActionBar();
        public void closeSkillActionWindow() => skillMenu.closeActionBar();

        //Skill Combos

        public int getCombineCount() => skillMenu.skillCount;
        public bool skillComboSelected(int index) => skillMenu.selectSkill(index);

        public void removeSkill(int index) => skillMenu.back(index);

        /// Private Functions
        //Party Targeting
        private void hidePartyMembers()
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(false);
            }
        }
        private void openPartyTargetWindow(PartyTargetSelections selectionType, ObjectData data, Stats user)
        {
            PartyTargetWindow.SetActive(true);
            partyTargetMenu.initTargets(selectionType, data, user);
        }
    }
}
