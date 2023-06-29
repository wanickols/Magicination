using UnityEngine;

namespace Core
{
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

        //Animator
        public static bool IsAnimating(this Animator animator, int layer = 0) => animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1;


    }
}