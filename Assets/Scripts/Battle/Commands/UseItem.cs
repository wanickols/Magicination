using System.Collections;
using System.Collections.Generic;

namespace Battle
{
    public class UseItem : ICommand
    {

        private List<Actor> targets;
        private IConsumable item;

        public UseItem(List<Actor> targets, IConsumable item)
        {
            this.targets = targets;
            this.item = item;
        }

        public bool isFinished { get; private set; } = false;
        public IEnumerator Co_Execute()
        {
            foreach (var target in targets)
            {
                BattleCalculations.useItem(target, item);
            }
            yield return null;
            isFinished = true;
        }
    }
}