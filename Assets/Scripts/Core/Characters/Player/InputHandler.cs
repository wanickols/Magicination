using UnityEngine;

public class InputHandler
{
    private Player player;
    private enum Command
    {
        None,
        MoveLeft, MoveRight, MoveUp, MoveDown,
    }

    Command command;
    public InputHandler(Player player)
    {
        this.player = player;
    }

    public void CheckInput()
    {

        command = Command.None;

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

        HandleCommand();
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

        }
    }

    //Currently it just moves it, but might add more later so abstracted it
    private void ProcessMovementInput(Vector2Int direction)
    {
        player.mover.Move(direction);
    }
}
