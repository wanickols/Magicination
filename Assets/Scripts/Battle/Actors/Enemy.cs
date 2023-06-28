using System.Collections;
using UnityEngine;
namespace Battle
{
    public class Enemy : Actor
    {
        protected override void Start()
        {
            base.Start();
            float offset = -2f;
            targetPosition.x = startingPosition.x + offset;
        }

        public override void StartTurn()
        {
            isTakingTurn = true;

            StartCoroutine(Co_MoveToAttack());
        }

        protected override IEnumerator Co_MoveToAttack()
        {
            float elapsedTime = 0;

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.Lerp(startingPosition, targetPosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

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