using Core;
using UnityEngine;


//Extending Grid to work with 2d and not 3d vectors
public static class Extensions
{

    public static Vector2Int GetCell2D(this Grid grid, GameObject gameObject)
    {
        Vector3 position = gameObject.transform.position;

        return (Vector2Int)grid.WorldToCell(position);
    }

    public static Vector2 GetCellCenter2D(this Grid grid, Vector2Int cell)
    {
        Vector3Int threeDimenCell = new Vector3Int(cell.x, cell.y, 0);

        return (Vector2)grid.GetCellCenterWorld(threeDimenCell);
    }

    public static Vector2 Center2D(this Grid grid, Vector2Int cell)
    {
        Vector3Int threeDimenCell = new Vector3Int(cell.x, cell.y, 0);

        return grid.GetCellCenterWorld(threeDimenCell);
    }

    //Directions
    public static Vector2Int getVector(this Dir dir)
    {
        Vector2Int direction = dir switch
        {
            Dir.Up => Direction.Up,
            Dir.Down => Direction.Down,
            Dir.Right => Direction.Right,
            Dir.Left => Direction.Left,
            _ => new Vector2Int(0, 0),
        };

        return direction;
    }

    //Animator
    public static bool IsAnimating(this Animator animator, int layer = 0) => animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1;
}

