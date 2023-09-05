using UnityEngine;

namespace MGCNTN
{
    public class SkillNode : MonoBehaviour
    {
        public Vector2Int coord;
        public RectTransform rectTransform;

        private void Awake() => rectTransform = GetComponent<RectTransform>();

        private SkillNode leftNeighbor, rightNeighbor, upNeighbor, downNeighbor; //neighbors

        public void AddNeighbor(SkillNode node, Dir dir)
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

        public SkillNode getNeighbor(Dir dir)
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
