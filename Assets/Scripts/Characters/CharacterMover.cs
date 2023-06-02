using System.Collections;
using UnityEngine;

public class CharacterMover
{

    private Character character;
    private Transform transform;
    public bool isMoving = false;
    public Vector2Int currCell => Game.Map.grid.GetCell2D(character.gameObject);
    private const float TIME_TO_MOVE_ONE_CELL = .375f;

    public CharacterMover(Character character)
    {
        this.character = character;
        this.transform = character.transform;
    }

    public void TryMove(Vector2Int direction)
    {
        if (isMoving || !direction.IsBasic())
            return;

        character.turn.Turn(direction);

        if (CanMove(direction))
        {
            Game.Map.occupiedCells.Add((currCell + direction), character);
            Game.Map.occupiedCells.Remove(currCell);
            character.StartCoroutine(Co_Move(direction));
        };

    }

    private bool CanMove(Vector2Int direction)
    {
        //Character Check
        if (isOccupied(direction)) //Not moving into occupied cell
            return false;

        //Tilemap Check
        Ray2D ray = new Ray2D(currCell.Center2D(), direction);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        Debug.DrawRay(ray.origin, ray.direction, new Color(20, 20, 255), 2f);

        Vector2Int distance = (currCell + direction) - currCell;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.distance <= Game.Map.grid.cellSize.x)
                return false;
        }



        return true;

    }

    private bool isOccupied(Vector2Int direction) => Game.Map.occupiedCells.ContainsKey(currCell + direction);

    private IEnumerator Co_Move(Vector2Int direction)
    {
        isMoving = true;


        Vector2 startingPosition = currCell.Center2D();
        Vector2 endingPosition = (currCell + direction).Center2D();



        float elapsedTime = 0;

        while ((Vector2)transform.position != endingPosition)
        {
            //How long takes to move
            transform.position = Vector2.Lerp(startingPosition, endingPosition, elapsedTime / TIME_TO_MOVE_ONE_CELL);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endingPosition;

        isMoving = false;

    }
}