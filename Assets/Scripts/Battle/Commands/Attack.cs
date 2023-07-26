using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Attack : ICommand
    {

        private Actor attacker;
        private List<Actor> targets;
        private float moveSpeed = 2f;

        public bool isFinished { get; private set; } = false;
        public Attack(Actor actor, List<Actor> targets)
        {
            this.attacker = actor;
            this.targets = targets;
        }



        public IEnumerator Co_Execute()
        {

            Vector3 targetPos = targets[0].transform.position;
            Vector3 offset = new Vector3(.5f, 0, 0);

            if (targets[0].GetType() == typeof(Enemy))
            {
                offset.x = -.5f;
            }

            targetPos = targetPos + offset;

            if (attacker.animator)
                attacker.animator.Play("Moving");
            while (attacker.transform.position != targetPos)
            {
                attacker.transform.position
                    = Vector2.MoveTowards(attacker.transform.position,
                    targetPos, moveSpeed * Time.deltaTime);

                yield return null;
            }



            if (attacker.animator)
            {
                attacker.animator.Play("Attack");
                do
                {
                    yield return null;
                }
                while (attacker.animator.IsAnimating());

                attacker.animator.Play("Idle");
            }

            foreach (Actor target in targets)
            {
                Combat.Attack(attacker, target);// Change to foreach eventually
            }
            isFinished = true;
        }
    }
}
