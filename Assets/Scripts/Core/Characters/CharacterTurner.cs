using UnityEngine;
namespace Core
{
    public class CharacterTurner
    {
        private MapLocation playerLocation;
        CharacterMover mover;
        public Vector2Int Facing { get; private set; } = Direction.Down;

        public CharacterTurner(CharacterMover mover, MapLocation playerLocation)
        {
            this.playerLocation = playerLocation;
            this.mover = mover;
        }

        public void Turn(Vector2Int direction)
        {
            if (direction.IsBasic())
                Facing = direction;
        }

        public void TurnAround() => Facing = new Vector2Int(-Facing.x, -Facing.y);

        public void TurnToPlayer()
        {

            Vector2Int location = playerLocation.location;

            if (location.x > mover.currCell.x)
                Turn(Direction.Right);

            else if (location.x < mover.currCell.x)
                Turn(Direction.Left);

            else if (location.y > mover.currCell.y)
                Turn(Direction.Up);

            else
                Turn(Direction.Down);
        }
    }
}
