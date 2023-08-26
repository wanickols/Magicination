using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MGCNTN.Core
{
    [System.Serializable]
    public class MoveCharacter : ICutCommand
    {

        [SerializeField] private Character character;
        [SerializeField] private float speed; //TODO make this effect the walkspeed
        [SerializeField] private List<Dir> route = new List<Dir>();

        public bool isFinished { get; private set; } = false;

        public IEnumerator CO_Execute()
        {
            foreach (Dir dir in route)
            {
                character.mover.TryMove(dir.getVector());

                yield return null;
                while (character.isMoving)
                    yield return null;

            }

            isFinished = true;
        }

        public override string ToString() => "Move Character";
    }
}
