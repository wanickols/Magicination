using System;
using UnityEngine;
namespace Core
{
    public class InputHandler
    {
        private Player player;
        private MainMenu mainMenu;
        private MapManager mapManager;
        private Map map => mapManager.map;
        private StateManager stateManager;

        private enum Command
        {
            None,
            MoveLeft, MoveRight, MoveUp, MoveDown,
            Interact,
            Continue, //For Dialogue
            OpenMenu,
        }

        //Events
        public event Action Continue;


        Command command;
        public InputHandler(Player player, MainMenu menu, MapManager mapManager, StateManager stateManager)
        {
            this.player = player;
            this.mainMenu = menu;
            this.mapManager = mapManager;
            this.stateManager = stateManager;
        }

        public void CheckInput()
        {
            command = Command.None;

            switch (stateManager.State)
            {
                case GameState.Cutscene:
                case GameState.Battle:
                case GameState.Menu:
                case GameState.Transition:
                    break;
                case GameState.Dialogue:
                    command = ContinueDialogueCheck();
                    break;
                case GameState.World:
                default:
                    command = GetMovementCommand();
                    break;
            }

            HandleCommand();
        }

        private Command GetMovementCommand()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                return Command.OpenMenu;

            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                return Command.MoveUp;

            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                return Command.MoveDown;

            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                return Command.MoveLeft;

            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                return Command.MoveRight;

            else if (Input.GetKeyDown(KeyCode.E))
                return Command.Interact;

            return Command.None;
        }

        private Command ContinueDialogueCheck()
        {

            if (Input.GetKeyUp(KeyCode.Space))
                return Command.Continue;


            return Command.None;
        }


        //Converts command to direction
        private void HandleCommand()
        {
            switch (command)
            {
                case (Command.MoveUp):
                    ProcessMovementInput(Direction.Up);
                    break;
                case (Command.MoveDown):
                    ProcessMovementInput(Direction.Down);
                    break;
                case (Command.MoveLeft):
                    ProcessMovementInput(Direction.Left);
                    break;
                case (Command.MoveRight):
                    ProcessMovementInput(Direction.Right);
                    break;
                case (Command.Interact):
                    ProccessInteract();
                    break;
                case (Command.OpenMenu):
                    ProccessOpenMenu();
                    break;
                case (Command.Continue):
                    Continue?.Invoke();
                    break;
            }
        }

        //Currently it just moves it, but might add more later so abstracted it
        private void ProcessMovementInput(Vector2Int direction)
        {
            player.mover.TryMove(direction);
        }

        private void ProccessInteract()
        {

            Vector2Int targetCell = player.facing + player.currCell;


            if (!map.containsKey(targetCell))
                return;

            IInteractable interactable = map.isInteractable(targetCell);

            if (interactable != null)
                interactable.Interact();
        }
        private void ProccessOpenMenu()
        {
            mainMenu.Open();
        }
    }
}