using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class PauseMenuSelection : SelectionManager
    {
        ///Private Parameters
        //Variables
        private MainWindow mainWindow;
        private PauseMenu pauseMenu;

        //States
        private Selector mainSelector => pauseMenu.mainSelector;
        private Dictionary<MenuState, Selector> stateSelector = new Dictionary<MenuState, Selector>();
        private MenuState menuState = MenuState.Main;
        private MenuState prevState;

        /// Public Parameters
        public override Selector CurrentSelector => stateSelector.ContainsKey(menuState) ? stateSelector[menuState] : null;

        /// Public Functions
        //Constructor
        public PauseMenuSelection(MainWindow mainWindow, PauseMenu pauseMenu)
        {
            this.mainWindow = mainWindow;
            this.pauseMenu = pauseMenu;

            //Selectors to Enum
            //Main
            stateSelector.Add(MenuState.Main, mainSelector);
            stateSelector.Add(MenuState.MemberSelection, mainWindow.memberSelector);

            //Items
            stateSelector.Add(MenuState.ItemSelection, mainWindow.itemSelector);
            stateSelector.Add(MenuState.ItemActionSelection, mainWindow.itemActionSelection);

            //Skills
            stateSelector.Add(MenuState.SkillCateogrySelection, mainWindow.skillCategoryBar);
            stateSelector.Add(MenuState.SkillSelection, mainWindow.skillSelector);
            stateSelector.Add(MenuState.SkillActionSelection, mainWindow.skillActionSelector);
            stateSelector.Add(MenuState.SkillCombinationSelection, mainWindow.skillCombinationSelector);

            //Equip
            stateSelector.Add(MenuState.EquipmentSelection, mainWindow.equipmentSelector);
            stateSelector.Add(MenuState.EquippableSelection, mainWindow.equippableSelector);

            //Party
            stateSelector.Add(MenuState.PartyTargetSelection, mainWindow.partyMemberSelector);

        }


        ///Abstract Implmentations
        public override void Cancel()
        {
            switch (menuState)
            {
                //Main
                case (MenuState.Main):
                    pauseMenu.Close();
                    break;
                case (MenuState.MemberSelection):
                    returnToMain();
                    break;

                //Items
                case (MenuState.ItemActionSelection):
                    returnToMain();
                    mainWindow.closeItemView();
                    mainWindow.ShowDefaultView();
                    break;
                case (MenuState.ItemSelection):
                    SetMenuState(MenuState.ItemActionSelection, true);
                    break;

                //Skillss
                case (MenuState.SkillCateogrySelection):
                    returnToMembers();
                    mainWindow.closeSkillView();
                    mainWindow.ShowDefaultView();
                    break;
                case (MenuState.SkillSelection):
                    SetMenuState(MenuState.SkillCateogrySelection, true);
                    break;
                case (MenuState.SkillActionSelection):
                    mainWindow.closeSkillActionWindow();
                    SetMenuState(MenuState.SkillSelection, true);
                    break;
                case (MenuState.SkillCombinationSelection):
                    SetMenuState(MenuState.SkillActionSelection, true);
                    break;

                //Equip
                case (MenuState.EquipmentSelection):
                    returnToMembers();
                    break;
                case (MenuState.EquippableSelection):
                    mainWindow.hideEquippableSelection(CurrentSelector);
                    SetMenuState(MenuState.EquipmentSelection, true);
                    break;

                //Party
                case (MenuState.PartyTargetSelection):
                    if (prevState == MenuState.SkillActionSelection)
                        mainWindow.ShowSkillActionWindow();

                    SetMenuState(prevState, true);
                    mainWindow.closePartyTargetWindow();
                    break;

            }
        }
        public override void Accept()
        {
            switch (menuState)
            {
                //Main
                case (MenuState.Main):
                    ProcessMainSelection();
                    break;
                case (MenuState.MemberSelection):
                    ProcessMemberSelection();
                    break;

                //Items
                case (MenuState.ItemActionSelection):
                    if (mainWindow.itemActionSelected(CurrentSelector.SelectedIndex)) //accounts for sorting
                        SetMenuState(MenuState.ItemSelection, true);
                    break;
                case (MenuState.ItemSelection):
                    mainWindow.itemSelected(CurrentSelector.SelectedIndex);
                    SetMenuState(MenuState.PartyTargetSelection, false);
                    break;

                //Skills
                case (MenuState.SkillCateogrySelection):
                    SetMenuState(MenuState.SkillSelection, true);
                    break;
                case (MenuState.SkillSelection):
                    mainWindow.ShowSkillActionWindow();
                    SetMenuState(MenuState.SkillActionSelection, false);
                    break;
                case (MenuState.SkillActionSelection):
                    if (CurrentSelector.SelectedIndex == 0)
                        SetMenuState(MenuState.SkillCombinationSelection, false);
                    else
                    {
                        mainWindow.skillSelected();
                        SetMenuState(MenuState.PartyTargetSelection, false);
                    }
                    break;
                case (MenuState.SkillCombinationSelection):
                    Debug.Log("Combinin'");
                    Cancel();
                    break;

                //Equip
                case (MenuState.EquipmentSelection):
                    int selected = CurrentSelector.SelectedIndex;
                    SetMenuState(MenuState.EquippableSelection, false);
                    mainWindow.ShowArsenalView(CurrentSelector, selected);
                    break;
                case (MenuState.EquippableSelection):
                    mainWindow.swapEquippable(CurrentSelector);
                    Cancel();
                    break;

                //Party
                case (MenuState.PartyTargetSelection):
                    mainWindow.partyTargetSelected(CurrentSelector.SelectedIndex);
                    Cancel();
                    break;
            }

            checkHover();
        }
        public override void checkHover()
        {
            switch (menuState)
            {
                //Equip
                case MenuState.EquipmentSelection:
                case MenuState.EquippableSelection:
                    mainWindow.updateEquipmentStats(CurrentSelector);
                    break;

                //Skills
                case MenuState.SkillCateogrySelection:
                    mainWindow.ShowSkillTree(CurrentSelector.SelectedIndex);
                    break;
                case MenuState.SkillSelection:
                    mainWindow.skillDescription();
                    break;

                default:
                    break;
            }

        }

        /// Private Functions
        //Selections
        private void ProcessMainSelection()
        {
            switch ((mainSelections)mainSelector.SelectedIndex)
            {
                case mainSelections.Items:
                    mainWindow.ShowItemView();
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
                    mainWindow.ShowSkillView(CurrentSelector.SelectedIndex);
                    SetMenuState(MenuState.SkillCateogrySelection, false);
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

        //Cancels
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

        //General
        private void SetMenuState(MenuState newState, bool cancel)
        {
            if (!cancel)
                CurrentSelector.setAnimation(false);


            CurrentSelector.gameObject.SetActive(!cancel);

            if (stateSelector.ContainsKey(newState))
            {
                prevState = menuState;
                menuState = newState;

                CurrentSelector.gameObject.SetActive(true);

                if (!cancel)
                    CurrentSelector.SelectedIndex = 0;

            }
        }
    }

}
