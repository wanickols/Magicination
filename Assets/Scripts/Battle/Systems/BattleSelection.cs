using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{

    [Serializable]
    public class BattleSelection : SelectionManager
    {
        /// Public Parameter
        public override Selector CurrentSelector => stateSelector.ContainsKey(menuState) ? stateSelector[menuState] : null;
        public Consumable currItem;
        public BattleMainSelections currBattleSelection;

        /// Private Parameters
        [Header("Selectors")]
        [SerializeField] private Selector mainSelector;
        [SerializeField] private Selector itemSelector;
        //[SerializeField] private Selector skillSelector;

        //States
        private BattleMenuStates menuState = BattleMenuStates.mainSelection;
        private Dictionary<BattleMenuStates, Selector> stateSelector = new Dictionary<BattleMenuStates, Selector>();

        //Managers
        private BattleWindow battleWindow;
        private BattleUIManager manager;

        /// Public Functions
        public void init(BattleWindow battleWindow, BattleUIManager manager)
        {
            this.manager = manager;
            this.battleWindow = battleWindow;
            //Connecting selectors to states
            stateSelector.Add(BattleMenuStates.mainSelection, mainSelector);
            stateSelector.Add(BattleMenuStates.itemSelection, itemSelector);
            //stateSelector.Add(BattleMenuStates.skillSelection, skillSelector);
        }

        //Overrides
        public override bool Accept()
        {
            switch (menuState)
            {
                case BattleMenuStates.mainSelection:
                    ProcessMainSelection();
                    break;
                case BattleMenuStates.itemSelection:
                    currItem = battleWindow.getItem(CurrentSelector.SelectedIndex);
                    currBattleSelection = BattleMainSelections.Items;
                    battleWindow.closeItemWindow();
                    manager.trySelect();
                    break;
                case BattleMenuStates.skillSelection:
                    break;
            }

            return true;
        }
        public override void Cancel()
        {
            switch (menuState)
            {
                case BattleMenuStates.mainSelection:
                    break;
                case BattleMenuStates.itemSelection:
                    battleWindow.closeItemWindow();
                    returnToMain();
                    break;
                case BattleMenuStates.skillSelection:
                    returnToMain();
                    break;
            }
        }
        public override void checkHover() { }

        public void revertSelection()
        {
            Battle.battleState = BattleStates.battle;
            manager.inputAllowed();
            manager.setBattleMenu(true);

            switch (menuState)
            {
                case BattleMenuStates.mainSelection:
                    break;
                case BattleMenuStates.itemSelection:
                    battleWindow.ShowItemWindow();
                    break;
            }
        }

        public void returnToMain(bool cancel = true) => SetMenuState(BattleMenuStates.mainSelection, cancel);

        /// Private Functions
        private void ProcessMainSelection()
        {
            switch ((BattleMainSelections)mainSelector.SelectedIndex)
            {
                case BattleMainSelections.Attack:
                    currBattleSelection = BattleMainSelections.Attack;
                    manager.trySelect();
                    break;
                case BattleMainSelections.Items:
                    battleWindow.ShowItemWindow();
                    SetMenuState(BattleMenuStates.itemSelection, false);
                    break;
                case BattleMainSelections.Skills:
                //SetMenuState(BattleMenuStates.skillSelection, false);
                case BattleMainSelections.Run:
                    manager.tryRun();
                    break;
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }
        private void SetMenuState(BattleMenuStates newState, bool cancel)
        {
            if (!cancel)
                CurrentSelector.setAnimation(false);


            CurrentSelector.gameObject.SetActive(!cancel);

            if (stateSelector.ContainsKey(newState))
            {
                menuState = newState;

                CurrentSelector.gameObject.SetActive(true);
                if (!cancel)
                    CurrentSelector.SelectedIndex = 0;



            }


        }




    }
}
