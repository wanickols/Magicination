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
            //Init
            Animator anim = attackerGFX.anim;
            float moveSpeed = attackerGFX.animationSpeed;
            Vector3 targetPos = targets[0].transform.position;
            Vector3 offset = new Vector3(.5f, 0, 0);

            if (targets[0].type == ActorType.Enemy)
                offset.x = -.5f;

            targetPos = targetPos + offset;

            //Start
            if (anim)
                anim.Play("Moving");

            //Move
            while (attackerGFX.currPosition != (Vector2)targetPos)
            {
                attacker.transform.position
                    = Vector2.MoveTowards(attackerGFX.currPosition,
                    targetPos, moveSpeed * Time.deltaTime);

                yield return null;
            }


            //Atack
            if (anim)
            {
                anim.Play("Attack");

                do yield return null;
                while (anim.IsAnimating());

                anim.Play("Idle");
            }

            //Calculate Damage
            foreach (Actor target in targets)
                BattleCalculations.Attack(attacker, target);// Change to foreach eventually

            //Finish
            isFinished = true;
        }
    }
}
