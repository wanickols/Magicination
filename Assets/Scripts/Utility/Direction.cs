using UnityEngine;

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

    public static Vector2 Center2D(this Vector2Int cell)
    {
        Vector3Int threeDimenCell = new Vector3Int(cell.x, cell.y, 0);

        return (Vector2)Game.manager.GetCellCenterWorld(threeDimenCell);
    }

    //Four Main directions
    public static readonly Vector2Int Up = new Vector2Int(0, 1);
    public static readonly Vector2Int Down = new Vector2Int(0, -1);
    public static readonly Vector2Int Left = new Vector2Int(-1, 0);
    public static readonly Vector2Int Right = new Vector2Int(1, 0);
}
