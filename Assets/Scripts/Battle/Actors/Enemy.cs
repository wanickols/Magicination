using System.Collections;
using UnityEngine;
namespace Battle
{
    public class Enemy : Actor
    {

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            offset = -.4f;
            base.Start();
        }

        public override void StartTurn()
        {
            isTakingTurn = true;

            StartCoroutine(Co_MoveToAttack());
        }

        protected override IEnumerator Co_MoveToAttack()
        {
            yield return base.Co_MoveToAttack();
            StartCoroutine(Co_EnemyChooseAction());
        }

        private IEnumerator Co_EnemyChooseAction()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("Command Accepted");
                    break;
                }
                yield return null;
            }

            StartCoroutine(Co_MoveToStarting());

        }
    }
}