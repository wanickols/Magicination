using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {

        /// Private Paremeters
        [SerializeField] private GameObject EquipWindow, arsenalWindow;

        private EquipMenu equipMenu;
        private ArsenalMenu arsenalMenu;




        /// Unity Functions
        void Start()
        {
            ShowDefaultView();
            equipMenu = EquipWindow.GetComponent<EquipMenu>();
            arsenalMenu = arsenalWindow.GetComponent<ArsenalMenu>();
            arsenalMenu.init(equipMenu);
        }


        /// Public

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


            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(false);
            }

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

    }
}
