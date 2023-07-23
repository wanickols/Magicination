using System.Collections;
using UnityEngine;

namespace Battle
{

    public abstract class Actor : MonoBehaviour
    {
        //Store actor's stats and methods for taking a turn
        protected Vector2 startingPosition;
        protected Vector2 targetPosition;

        //Battle Data
        public Stats Stats { get; set; }
        public Sprite battlePortrait { get; private set; }

        //Turn Related
        public float baseTurnSpeed => Stats.turnSpeed;
        public float turnTime = 0;
        public bool isTakingTurn { get; protected set; } = false;

        //Animation and movement
        [SerializeField] protected float offset;
        [SerializeField] protected float attackAnimationSpeed = 2f;


        protected virtual void Start()
        {
            turnTime = baseTurnSpeed;
            targetPosition = startingPosition = transform.position;
            targetPosition += new Vector2(offset, 0);

        }
        public abstract void StartTurn();

        protected virtual IEnumerator Co_MoveToAttack()
        {
            float elapsedTime = 0;

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.Lerp(startingPosition, targetPosition, elapsedTime);
                elapsedTime += (Time.deltaTime * attackAnimationSpeed);
                yield return null;
            }
        }

        protected virtual IEnumerator Co_MoveToStarting()
        {
            float elapsedTime = 0;

            while ((Vector2)transform.position != startingPosition)
            {
                transform.position = Vector2.Lerp(transform.position, startingPosition, elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isTakingTurn = false;
        }

        public void setBattleData(Stats stats, Sprite sprite)
        {
            this.Stats = stats;
            this.battlePortrait = sprite;
        }
    }
}
