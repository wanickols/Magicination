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
        private Battle battle;

        /// Public Functions
        public void init(BattleWindow battleWindow, Battle manager)
        {
            battle = manager;
            this.battleWindow = battleWindow;
            //Connecting selectors to states
            stateSelector.Add(BattleMenuStates.mainSelection, mainSelector);
            stateSelector.Add(BattleMenuStates.itemSelection, itemSelector);
            //stateSelector.Add(BattleMenuStates.skillSelection, skillSelector);
        }

        //Overrides
        public override void Accept()
        {
            switch (menuState)
            {
                case BattleMenuStates.mainSelection:
                    ProcessMainSelection();
                    break;
                case BattleMenuStates.itemSelection:
                    currItem = battleWindow.getItem(CurrentSelector.SelectedIndex);
                    battle.trySelect(BattleMainSelections.Items);
                    Cancel();
                    break;
                case BattleMenuStates.skillSelection:
                    break;
            }
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

        /// Private Functions
        private void ProcessMainSelection()
        {
            switch ((BattleMainSelections)mainSelector.SelectedIndex)
            {
                case BattleMainSelections.Attack:
                    battle.trySelect(BattleMainSelections.Attack);
                    break;
                case BattleMainSelections.Items:
                    battleWindow.ShowItemWindow();
                    SetMenuState(BattleMenuStates.itemSelection, false);
                    break;
                case BattleMainSelections.Skills:
                //SetMenuState(BattleMenuStates.skillSelection, false);
                case BattleMainSelections.Run:
                    battle.tryRun();
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

                if (!cancel)
                    CurrentSelector.SelectedIndex = 0;

                CurrentSelector.gameObject.SetActive(true);

            }


        }
        private void returnToMain()
        {
            SetMenuState(BattleMenuStates.mainSelection, true);
        }

    }
}
