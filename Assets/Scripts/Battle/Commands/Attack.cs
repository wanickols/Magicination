using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class Attack : ICommand
    {

        private Actor attacker;
        private ActorGraphics attackerGFX;
        private List<Actor> targets;

        public bool isFinished { get; private set; } = false;
        public Attack(Actor actor, List<Actor> targets)
        {
            this.attacker = actor;
            attackerGFX = actor.gfx;
            this.targets = targets;
        }



        public IEnumerator Co_Execute()
        {
            float moveSpeed = attackerGFX.animationSpeed;
            Vector3 targetPos = targets[0].transform.position;
            Vector3 offset = new Vector3(.5f, 0, 0);

            if (targets[0].GetType() == typeof(Enemy))
            {
                offset.x = -.5f;
            }

            targetPos = targetPos + offset;

            if (attackerGFX.animator)
                attackerGFX.animator.Play("Moving");
            while (attackerGFX.currPosition != (Vector2)targetPos)
            {
                attacker.transform.position
                    = Vector2.MoveTowards(attackerGFX.currPosition,
                    targetPos, moveSpeed * Time.deltaTime);

                yield return null;
            }



            if (attackerGFX.animator)
            {
                attackerGFX.animator.Play("Attack");
                do
                {
                    yield return null;
                }
                while (attackerGFX.animator.IsAnimating());

                attackerGFX.animator.Play("Idle");
            }

            foreach (Actor target in targets)
            {
                BattleCalculations.Attack(attacker, target);// Change to foreach eventually
            }
            isFinished = true;
        }
    }
}
