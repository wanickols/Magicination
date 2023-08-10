using System.Collections;
using UnityEngine;

namespace Core
{
    public class CharacterMover
    {

        private Character character;
        private Transform transform;
        public bool isMoving = false;
        public Vector2Int currCell => map.GetCell2D(character.gameObject);
        private const float TIME_TO_MOVE_ONE_CELL = .375f;
        private Map map => Game.manager.mapManager.map;



        public CharacterMover(Character character)
        {
            this.character = character;
            this.transform = character.transform;
        }

        public void TryMove(Vector2Int direction)
        {
            if (isMoving || !direction.IsBasic())
                return;

            character.turner.Turn(direction);

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


            foreach (RaycastHit2D hit in hits)
                if (hit.distance <= map.cellsize)
                    return false;

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
            character.setCurrCell();

            if (character is Player player)
                player.CheckCurrentCell(map);

            isMoving = false;
        }

        public void setUpStartingCell()
        {
            //Puts characters in center of tile at spawn
            character.transform.position = map.GetCellCenter2D(currCell);
            map.addCell(currCell, character);
        }
    }
}
