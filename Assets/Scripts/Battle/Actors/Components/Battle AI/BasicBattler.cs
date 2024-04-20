
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class BasicBattler : ActorAI
    {
        public override ICommand ChooseAction() => singleTargetAttack();

        private ICommand singleTargetAttack()
        {
            List<Actor> targets = new List<Actor>();
            //Single Target 
            int randomTarget = Random.Range(0, possibleTargets.Count);
            targets.Add(possibleTargets[randomTarget]);

            return new Attack(actor, targets);
        }
    }
}
