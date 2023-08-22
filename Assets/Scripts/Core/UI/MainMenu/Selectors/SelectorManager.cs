using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SelectorManager
    {
        //Variables
        private MainWindow mainWindow;
        private PauseMenu pauseMenu;

        //Selectors
        private Selector mainSelector => pauseMenu.mainSelector;
        private Selector memberSelector => mainWindow.memberSelector;
        private Selector equipmentSelector => mainWindow.equipmentSelector;
        private Selector equippableSelector => mainWindow.equippableSelector;
        private Selector itemActionSelector => mainWindow.itemActionBar;

        //States
        private Dictionary<MenuState, Selector> stateSelector = new Dictionary<MenuState, Selector>();



        /// Public Parameters
        public Selector CurrentSelector => stateSelector.ContainsKey(menuState) ? stateSelector[menuState] : null;
        private MenuState menuState;
        private MenuState prevState;

        public SelectorManager(MainWindow mainWindow, PauseMenu pauseMenu)
        {
            this.mainWindow = mainWindow;
            this.pauseMenu = pauseMenu;

            //Connecting selectors to states
            stateSelector.Add(MenuState.Main, mainSelector);
            stateSelector.Add(MenuState.MemberSelection, memberSelector);
            stateSelector.Add(MenuState.EquipmentSelection, equipmentSelector);
            stateSelector.Add(MenuState.EquippableSelection, equippableSelector);

        }

        public void addItemSelector(Selector itemSelector)
        {
            stateSelector.Add(MenuState.ItemSelection, itemSelector);
            stateSelector.Add(MenuState.ItemActionSelection, itemActionSelector);
        }
        public void removeItemSelector()
        {
            stateSelector.Remove(MenuState.ItemSelection);
            stateSelector.Remove(MenuState.ItemActionSelection);
        }

        public void addPartyTargetSelector(Selector targetSelector) => stateSelector.Add(MenuState.PartyTargetSelection, targetSelector);
        public void removePartyTargetSelector() => stateSelector.Remove(MenuState.PartyTargetSelection);

        public void Cancel()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    pauseMenu.Close();
                    break;
                case (MenuState.MemberSelection):
                    returnToMain();
                    break;
                case (MenuState.PartyTargetSelection):
                    mainWindow.closePartyTargetWindow(this);
                    SetMenuState(prevState, true);
                    break;
                case (MenuState.ItemActionSelection):
                    returnToMain();
                    mainWindow.closeItemView(this);
                    mainWindow.ShowDefaultView();
                    break;
                case (MenuState.ItemSelection):
                    SetMenuState(MenuState.ItemActionSelection, true);
                    break;
                case (MenuState.EquipmentSelection):
                    returnToMembers();
                    break;
                case (MenuState.EquippableSelection):
                    mainWindow.hideEquippableSelection(CurrentSelector);
                    SetMenuState(MenuState.EquipmentSelection, true);
                    break;


            }
        }
        public void Accept()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    ProcessMainSelection();
                    break;
                case (MenuState.MemberSelection):
                    ProcessMemberSelection();
                    break;
                case (MenuState.PartyTargetSelection):
                    mainWindow.partyTargetSelected(CurrentSelector.SelectedIndex);
                    Cancel();
                    break;
                case (MenuState.ItemActionSelection):
                    if (mainWindow.itemActionSelected(CurrentSelector.SelectedIndex)) //accounts for sorting
                        SetMenuState(MenuState.ItemSelection, true);
                    break;
                case (MenuState.ItemSelection):
                    mainWindow.itemSelected(CurrentSelector.SelectedIndex, this);
                    SetMenuState(MenuState.PartyTargetSelection, false);
                    break;
                case (MenuState.EquipmentSelection):
                    int selected = CurrentSelector.SelectedIndex;
                    SetMenuState(MenuState.EquippableSelection, false);
                    mainWindow.ShowArsenalView(CurrentSelector, selected);
                    break;
                case (MenuState.EquippableSelection):
                    mainWindow.swapEquippable(CurrentSelector);
                    Cancel();
                    break;
            }
        }

        /// Public Functions
        public void checkHover()
        {
            switch (menuState)
            {
                case MenuState.EquipmentSelection:
                case MenuState.EquippableSelection:
                    mainWindow.onHover(menuState, CurrentSelector);
                    break;
                default:
                    break;
            }

        }
        private void returnToMain()
        {
            SetMenuState(MenuState.Main, true);
            CurrentSelector.setAnimation(true);
        }
        private void returnToMembers()
        {
            mainWindow.ShowDefaultView();
            SetMenuState(MenuState.MemberSelection, true);
            CurrentSelector.setAnimation(true);
        }

        public void SetMenuState(MenuState newState, bool cancel)
        {
            if (!cancel)
                CurrentSelector.setAnimation(false);


            CurrentSelector.gameObject.SetActive(!cancel);

            if (stateSelector.ContainsKey(newState))
            {
                prevState = menuState;
                menuState = newState;

                if (!cancel)
                    CurrentSelector.SelectedIndex = 0;

                CurrentSelector.gameObject.SetActive(true);

            }


        }
        private void ProcessMainSelection()
        {
            switch ((mainSelections)mainSelector.SelectedIndex)
            {
                case mainSelections.Items:
                    mainWindow.ShowItemView(this);
                    SetMenuState(MenuState.ItemActionSelection, false);
                    break;
                case mainSelections.Skills:
                case mainSelections.Equip:
                case mainSelections.Status:
                case mainSelections.Order:
                    SetMenuState(MenuState.MemberSelection, false);
                    break;
                case mainSelections.Save:
                case mainSelections.Quit:
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }
        private void ProcessMemberSelection()
        {
            switch ((mainSelections)mainSelector.SelectedIndex)
            {
                case mainSelections.Skills:
                    Debug.LogWarning("Not implemented!");
                    break;
                case mainSelections.Equip:
                    mainWindow.ShowEquipmentView(CurrentSelector.SelectedIndex);
                    SetMenuState(MenuState.EquipmentSelection, false);
                    break;
                case mainSelections.Status:
                case mainSelections.Order:
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }



    }
}
