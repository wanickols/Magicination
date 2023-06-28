using System;
using UnityEngine;

public class InputHandler
{
    private Player player;
    private MainMenu mainMenu;
    private Map map;

    private enum Command
    {
        None,
        MoveLeft, MoveRight, MoveUp, MoveDown,
        Interact,
        Continue, //For Dialogue
        ToggleMenu,
    }

    //Events
    public event Action Continue;


    Command command;
    public InputHandler(Player player, MainMenu menu, Map map)
    {
        this.player = player;
        this.mainMenu = menu;
        this.map = map;

    }

    public void CheckInput()
    {
        command = Command.None;

        if (Game.manager.State == GameState.Cutscene)
            return;

        else if (Game.manager.State == GameState.Dialogue)
            command = ContinueDialogueCheck();

        else if (Input.GetKeyDown(KeyCode.Escape))
            command = Command.ToggleMenu;

        else if (Game.manager.State == GameState.World)
        {


            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                command = Command.MoveUp;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                command = Command.MoveDown;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                command = Command.MoveLeft;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                command = Command.MoveRight;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                command = Command.Interact;
            }

        }

        HandleCommand();
    }

    private Command ContinueDialogueCheck()
    {
        if (Game.manager.State == GameState.Dialogue)
        {
            if (Input.GetKeyUp(KeyCode.Space))
                return Command.Continue;
        }

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
            case (Command.ToggleMenu):
                ProcessToggleMenu();
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

        Vector2Int targetCell = player.facing + map.GetCell2D(player.gameObject);


        if (!map.containsKey(targetCell))
            return;

        IInteractable interactable = map.isInteractable(targetCell);
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    private void ProcessToggleMenu()
    {
        mainMenu.toggle();
    }
}
