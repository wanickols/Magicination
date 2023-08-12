
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class StateManager
    {
        public GameState State { get; private set; } = GameState.World;

        private Stack<GameState> stateHistory = new Stack<GameState>();

        //Game State Management

        public void restorePreviousState() => State = stateHistory.Pop();

        public void clearHistory() => stateHistory.Clear();

        public bool tryState(GameState state)
        {
            bool success = state switch
            {
                GameState.Battle => CanBattle(),
                GameState.Cutscene => CanCutscene(),
                GameState.Dialogue => CanDialogue(),
                GameState.Menu => CanMenu(),
                GameState.Transition => CanTransition(),
                GameState.World => CanWorld(),
                _ => false
            };

            if (!success)
            {
                Debug.LogWarning($"State change to {state} rejected!");
                return false;
            }
            changeState(state);

            return success;
        }

        private void changeState(GameState state)
        {
            stateHistory.Push(State);
            State = state;
        }

        private bool CanBattle()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => true,
                GameState.Dialogue => false,
                GameState.Menu => false,
                GameState.Transition => false,
                GameState.World => true,
                _ => false
            };
        }

        private bool CanCutscene()
        {
            return State switch
            {
                GameState.Battle => true,
                GameState.Cutscene => true,
                GameState.Dialogue => true,
                GameState.Menu => false,
                GameState.Transition => false,
                GameState.World => true,
                _ => false
            };
        }

        private bool CanDialogue()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => true,
                GameState.Dialogue => false,
                GameState.Menu => false,
                GameState.Transition => false,
                GameState.World => true,
                _ => false
            };
        }

        private bool CanMenu()
        {
            return State switch
            {
                GameState.Battle => false,
                GameState.Cutscene => false,
                GameState.Dialogue => false,
                GameState.Menu => false,
                GameState.Transition => false,
                GameState.World => true,
                _ => false
            };

        }

        private bool CanTransition()
        {
            return State switch
            {
                GameState.Battle => true,
                GameState.Cutscene => true,
                GameState.Dialogue => false,
                GameState.Menu => false,
                GameState.Transition => false,
                GameState.World => true,
                _ => false
            };
        }

        private bool CanWorld()
        {
            return State switch
            {
                GameState.Battle => true,
                GameState.Cutscene => true,
                GameState.Dialogue => true,
                GameState.Menu => true,
                GameState.Transition => true,
                GameState.World => false,
                _ => true
            };
        }
    }
}