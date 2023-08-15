using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        private GameObject EquipWindow, arsenalWindow;

        private EquipmentMenu equipMenu;

        EquippableType lastSelectedEquippableType = EquippableType.Weapon;

        void Start()
        {
            ShowDefaultView();
            equipMenu = EquipWindow.GetComponent<EquipmentMenu>();
        }

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
            Transform parent = equippableSelector.transform.parent;

            int count = equippables.Count;

            for (int i = 0; i < 16; i++)
            {
                EquippableOption option = parent.GetChild(i).gameObject.GetComponent<EquippableOption>();

                if (i < count)
                    option.changeOption(equippables[i]);
            }



        }

        public void hideEquippableSection()
        {
            arsenalWindow.SetActive(false);
        }

        public void swapEquippable(Selector equippableSelector)
        {
            Transform parent = equippableSelector.transform.parent;
            Equipment equipment = equipMenu.partyMember.equipment;

            EquippableOption option = parent.GetChild(equippableSelector.SelectedIndex).GetComponent<EquippableOption>();
            Equippable equippable = new Equippable();

            if (option.equippable != null)
                equippable = option.equippable;

            option.changeOption(equipment.getEquipped(lastSelectedEquippableType));
            equipment.Equip(equippable);

            clearEquippables(equippableSelector);

            updateEquipMenu();
        }

        public void clearEquippables(Selector equippableSelector)
        {
            Transform parent = equippableSelector.transform.parent;


            for (int i = 0; i < 16; i++)
            {
                EquippableOption option = parent.GetChild(i).gameObject.GetComponent<EquippableOption>();
                option.clear();
            }
        }

        private void updateEquipMenu() => equipMenu.updateValues();
    }
}
