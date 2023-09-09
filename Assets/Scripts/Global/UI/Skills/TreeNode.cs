using UnityEngine;

namespace MGCNTN
{
    public class TreeNode : MonoBehaviour
    {
        public Vector2Int coord;
        public RectTransform rectTransform;

        private void Awake() => rectTransform = GetComponent<RectTransform>();

        private TreeNode leftNeighbor, rightNeighbor, upNeighbor, downNeighbor; //neighbors

        public void AddNeighbor(TreeNode node, Dir dir)
        {
            switch (dir)
            {
                case Dir.Left:
                    leftNeighbor = node;
                    break;
                case Dir.Right:
                    rightNeighbor = node;
                    break;
                case Dir.Up:
                    upNeighbor = node;
                    break;
                case Dir.Down:
                    downNeighbor = node;
                    break;
            }
        }

        public TreeNode getNeighbor(Dir dir)
            => dir switch
            {
                Dir.Left => leftNeighbor,
                Dir.Right => rightNeighbor,
                Dir.Up => upNeighbor,
                Dir.Down => downNeighbor,
                _ => null,
            };

    }
}
