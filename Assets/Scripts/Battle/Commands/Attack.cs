using System.Collections;
using System.Collections.Generic;

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
            attacker = actor;
            this.targets = targets;
            attackerGFX = actor.gfx;
        }



        public IEnumerator Co_Execute()
        {
            foreach (Actor target in targets)
            {
                yield return attackerGFX.CO_AttackAnim(target);
                BattleCalculations.Attack(attacker, target);// Change to foreach eventually
            }
            //Finish
            isFinished = true;
        }
    }
}
