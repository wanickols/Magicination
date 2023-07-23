using System.Collections;

namespace Battle
{
    public class Ally : Actor
    {

        protected override void Start()
        {
            offset = .4f;
            base.Start();
        }

        public override void StartTurn()
        {
            isTakingTurn = true;
            Battle.Attack += attack;
            StartCoroutine(Co_MoveToAttack());
        }

        protected override IEnumerator Co_MoveToAttack()
        {
            yield return base.Co_MoveToAttack();

        }



        //Eventually will need more info, like which skill and such.
        private void attack(Actor target)
        {
            Battle.Attack -= attack;
            print(target.name + " Was Attacked");


            StartCoroutine(Co_MoveToStarting());
        }
    }
}
