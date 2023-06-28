using System.Collections.Generic;
using UnityEngine;

namespace Core
{

    public class NPC : Character, IInteractable
    {
        private enum Dir
        {
            Up,
            Down,
            Left,
            Right
        }


        [SerializeField] private List<Dir> moveRoute = new List<Dir>();
        [SerializeField] private float moveDelay = 0f;
        [SerializeField] private bool neverMoves = false;
        [SerializeField] private bool movesRandomely = false;
        [SerializeField] private bool loopRoute = false;
        [SerializeField] private NPCInfo NPCInfo = null;

        private int currRoutePos = 0;
        private float timeElapsed = 0f;

        public void Interact() => NPCInfo.createInteraction();


        protected override void Update()
        {
            base.Update();

            if (neverMoves || isMoving)
                return;

            timeElapsed += Time.deltaTime;
            if (timeElapsed < moveDelay)
                return;

            timeElapsed = 0f;

            if (movesRandomely)
            {
                MoveInRandomDirection();
                return;
            }

            ExecuteMoveRoute();

        }


        private void MoveInRandomDirection()
        {
            int random = UnityEngine.Random.Range(0, 4);

            Vector2Int moveDirection = random switch
            {
                0 => Direction.Up,
                1 => Direction.Down,
                2 => Direction.Left,
                3 => Direction.Right,
                _ => new Vector2Int(0, 0)
            };

            mover.TryMove(moveDirection);
        }


        private void ExecuteMoveRoute()
        {
            if (currRoutePos >= moveRoute.Count)
                return;

            Dir direction = moveRoute[currRoutePos];

            Vector2Int moveDirection = direction switch
            {
                Dir.Up => Direction.Up,
                Dir.Down => Direction.Down,
                Dir.Left => Direction.Left,
                Dir.Right => Direction.Right,
                _ => new Vector2Int(0, 0)

            };

            mover.TryMove(moveDirection);
            ++currRoutePos;

            if (loopRoute)
                currRoutePos = currRoutePos % moveRoute.Count;
        }
    }
}
