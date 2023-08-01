using UnityEngine;


namespace Core
{
    //Class to call static direction in any class
    public static class Direction
    {

        //Checks if this is one of the four basic directions
        public static bool IsBasic(this Vector2Int direction)
        {
            switch (direction)
            {
                case Vector2Int when direction.Equals(Direction.Up):
                case Vector2Int when direction.Equals(Direction.Down):
                case Vector2Int when direction.Equals(Direction.Left):
                case Vector2Int when direction.Equals(Direction.Right):
                    return true;
                default:
                    return false;
            }
        }

        //Four Main directions
        public static readonly Vector2Int Up = new Vector2Int(0, 1);
        public static readonly Vector2Int Down = new Vector2Int(0, -1);
        public static readonly Vector2Int Left = new Vector2Int(-1, 0);
        public static readonly Vector2Int Right = new Vector2Int(1, 0);


    }
}
