using System.Collections.Generic;

namespace Battle
{
    public class BasicBattler : BattlerAI
    {
        public override ICommand ChooseAction()
        {
            //TODO get actual target
            List<Actor> targets = new List<Actor>();
            targets.Add(FindObjectOfType<Ally>());
            return new Attack(actor, targets);
        }
    }
}
