using System.Collections;
using UnityEngine;

public class CharacterMover
{

    private Character character;
    private Transform transform;
    public bool isMoving = false;
    private Vector2Int currCell => Map.grid.GetCell2D(character.gameObject);
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
            Map.occupiedCells.Add((currCell + direction), character);
            Map.occupiedCells.Remove(currCell);
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


        if (hits.Length > 0)
            return false;



        return true;

    }

    private bool isOccupied(Vector2Int direction) => Map.occupiedCells.ContainsKey(currCell + direction);

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
