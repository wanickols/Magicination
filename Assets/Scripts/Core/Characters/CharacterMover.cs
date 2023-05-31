using System.Collections;
using UnityEngine;

public class CharacterMover
{

    private Character character;
    private Transform transform;
    public bool isMoving = false;
    private const float TIME_TO_MOVE_ONE_CELL = .375f;

    public CharacterMover(Character character)
    {
        this.character = character;
        this.transform = character.transform;
    }

    public void Move(Vector2Int direction)
    {
        if (direction.IsBasic() && !isMoving)
        {
            character.StartCoroutine(Co_Move(direction));
        }
    }

    private IEnumerator Co_Move(Vector2Int direction)
    {
        isMoving = true;
        character.turn.Turn(direction);

        var cellLocation = Map.grid.GetCell2D(character.gameObject);

        Vector2 startingPosition = cellLocation.Center2D();
        Vector2 endingPosition = (cellLocation + direction).Center2D();


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
