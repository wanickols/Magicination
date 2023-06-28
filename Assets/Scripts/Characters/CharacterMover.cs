using System.Collections;
using UnityEngine;

public class CharacterMover
{

    private Character character;
    private Transform transform;
    public bool isMoving = false;
    public Vector2Int currCell => map.GetCell2D(character.gameObject);
    private const float TIME_TO_MOVE_ONE_CELL = .375f;
    private Map map;

    public CharacterMover(Character character, Map map)
    {
        this.character = character;
        this.transform = character.transform;
        this.map = map;
    }

    public void TryMove(Vector2Int direction)
    {
        if (isMoving || !direction.IsBasic())
            return;

        character.turn.Turn(direction);

        if (CanMove(direction))
        {
            map.addCell((currCell + direction), character);
            map.removeCell(currCell);
            character.StartCoroutine(Co_Move(direction));
        };

    }

    private bool CanMove(Vector2Int direction)
    {
        //Character Check
        if (isOccupied(direction)) //Not moving into occupied cell
            return false;

        //Tilemap Check
        Ray2D ray = new Ray2D(map.grid.Center2D(currCell), direction);

        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        Debug.DrawRay(ray.origin, ray.direction, new Color(20, 20, 255), 2f);

        Vector2Int distance = (currCell + direction) - currCell;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.distance <= map.cellsize)
                return false;
        }



        return true;

    }

    private bool isOccupied(Vector2Int direction) => map.containsKey(currCell + direction);

    private IEnumerator Co_Move(Vector2Int direction)
    {
        isMoving = true;


        Vector2 startingPosition = map.grid.Center2D(currCell);
        Vector2 endingPosition = map.grid.Center2D(currCell + direction);



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
