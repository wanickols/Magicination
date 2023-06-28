using UnityEngine;

public class InputHandler
{
    private Player player;
    private enum Command
    {
        None,
        MoveLeft, MoveRight, MoveUp, MoveDown,
        Interact,
        Continue, //For Dialogue
        ToggleMenu,
    }

    Command command;
    public InputHandler(Player player)
    {
        this.player = player;

    }

    public void CheckInput()
    {
        command = Command.None;

        if (Game.State == GameState.Cutscene || Game.State == GameState.Dialogue)
            return;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            command = Command.ToggleMenu;
            HandleCommand();
            return;
        }


        if (Game.State == GameState.World)
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

    public bool ContinueDialogueCheck()
    {
        if (Game.State == GameState.Dialogue)
        {
            if (Input.GetKeyUp(KeyCode.Space))
                return true;
        }

        return false;
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
        }
    }


    //Currently it just moves it, but might add more later so abstracted it
    private void ProcessMovementInput(Vector2Int direction)
    {
        player.mover.TryMove(direction);
    }

    private void ProccessInteract()
    {

        Vector2Int targetCell = player.facing + Game.Map.grid.GetCell2D(player.gameObject);


        if (!Game.Map.occupiedCells.ContainsKey(targetCell))
            return;

        if (Game.Map.occupiedCells[targetCell] is IInteractable interactable)
        {
            interactable.Interact();
        }
    }
    private void ProcessToggleMenu()
    {
        Game.ToggleMenu();
    }

}
