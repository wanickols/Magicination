using System.Collections;
namespace Battle
{
    public class Enemy : Actor
    {
        protected override void Start()
        {
            offset = -.4f;
            base.Start();
        }



        protected override IEnumerator Co_MoveToAttack()
        {
            yield return base.Co_MoveToAttack();

        }


    }
}