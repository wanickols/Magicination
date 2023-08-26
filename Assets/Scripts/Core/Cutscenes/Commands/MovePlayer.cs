using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    [System.Serializable]
    public class MovePlayer : ICutCommand
    {

        [SerializeField] private float speed; //TODO make this effect the walkspeed
        [SerializeField] private List<Dir> route = new List<Dir>();

        public bool isFinished { get; private set; } = false;

        public IEnumerator CO_Execute()
        {
            Player player = Game.manager.player;

            foreach (Dir dir in route)
            {
                player.mover.TryMove(dir.getVector());

                yield return null;
                while (player.isMoving)
                    yield return null;


            }

            isFinished = true;
        }

        public override string ToString() => "Move Player";


    }
}