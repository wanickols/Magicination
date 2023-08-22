using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {


        /// Public Parameters
        public Selector memberSelector;
        public Selector equipmentSelector, equippableSelector;
        public Selector itemActionBar => itemMenu.actionSelector;

        /// Private Paremeters
        [SerializeField] private GameObject EquipWindow, arsenalWindow;
        [SerializeField] private GameObject ItemsWindow;
        [SerializeField] private GameObject PartyTargetWindow;


        private EquipMenu equipMenu;
        private ArsenalMenu arsenalMenu;
        private ItemMenu itemMenu;
        private PartyTargetMenu partyTargetMenu;

        /// Unity Functions
        void Start()
        {
            ShowDefaultView();
            equipMenu = EquipWindow.GetComponent<EquipMenu>();
            arsenalMenu = arsenalWindow.GetComponent<ArsenalMenu>();
            arsenalMenu.init(equipMenu);
            itemMenu = ItemsWindow.GetComponent<ItemMenu>();
            partyTargetMenu = PartyTargetWindow.GetComponent<PartyTargetMenu>();
        }

        /// Public Functions
        //General
        public void onHover(MenuState menuState, Selector selector)
        {
            switch (menuState)
            {
                case MenuState.EquippableSelection:
                    arsenalMenu.updateStats(selector);
                    break;
                default:
                    break;
            }
        }

        //Party Targets

        public void openPartyTargetWindow(SelectorManager manager, PartyTargetSelections selection, Consumable item = null)
        {
            PartyTargetWindow.SetActive(true);
            manager.addPartyTargetSelector(partyTargetMenu.initTargets(selection, item));
        }

        public void closePartyTargetWindow(SelectorManager manager)
        {
            PartyTargetWindow.SetActive(false);
            manager.removePartyTargetSelector();
        }

        public void partyTargetSelected(int selected)
        {
            partyTargetMenu.Select(selected);

            switch (partyTargetMenu.selectionType)
            {
                case PartyTargetSelections.item:
                    break;
                case PartyTargetSelections.skill:
                    break;
            }
        }

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
            List<Equippable> equippables = Party.arsenal.getEquippables((EquippableType)selected);

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

        //Item Menu
        public void ShowItemView(SelectorManager manager)
        {
            hidePartyMembers();
            ItemsWindow.SetActive(true);

            manager.addItemSelector(itemMenu.initItems());

        }

        public void closeItemView(SelectorManager manager)
        {
            ItemsWindow.SetActive(false);
            itemMenu.clearItems();
            manager.removeItemSelector();
        }

        public bool itemActionSelected(int selected) => itemMenu.setFlag(selected);
        public void itemSelected(int selected, SelectorManager manager)
        {


            Consumable item = itemMenu.selectItem(selected);

            openPartyTargetWindow(manager, PartyTargetSelections.item, item);

        }

        /// Private Functions
        private void hidePartyMembers()
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(false);
            }
        }
    }
}
