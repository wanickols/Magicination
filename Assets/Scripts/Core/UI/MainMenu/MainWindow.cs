using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {


        /// Public Parameters
        public Selector memberSelector;
        public Selector equipmentSelector, equippableSelector;

        /// Private Paremeters
        [SerializeField] private GameObject EquipWindow, arsenalWindow;
        [SerializeField] private GameObject ItemsWindow;


        private EquipMenu equipMenu;
        private ArsenalMenu arsenalMenu;
        private ItemMenu itemMenu;

        /// Unity Functions
        void Start()
        {
            ShowDefaultView();
            equipMenu = EquipWindow.GetComponent<EquipMenu>();
            arsenalMenu = arsenalWindow.GetComponent<ArsenalMenu>();
            arsenalMenu.init(equipMenu);
            itemMenu = ItemsWindow.GetComponent<ItemMenu>();
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
