using System.Collections;
using UnityEngine;

namespace Battle
{

    public abstract class Actor : MonoBehaviour
    {

        private BattlerAI ai;

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

        ///Publioc
        public void setBattleData(Stats stats, Sprite sprite)
        {
            this.Stats = stats;
            this.battlePortrait = sprite;
        }

        public virtual void StartTurn()
        {
            isTakingTurn = true;
            Battle.Attack += attack;
            StartCoroutine(Co_MoveToAttack());
        }

        //Battle

        //Eventually will need more info, like which skill and such.
        protected virtual void attack(Actor target)
        {

            Battle.Attack -= attack;
            print(target.name + " Was Attacked");


            StartCoroutine(Co_MoveToStarting());
        }

        public virtual void defense(Actor attacker)
        {
            print(attacker.name + " This bish attack me");
        }

        /// Protected

        protected virtual void Awake()
        {
            try
            {
                ai = GetComponent<BattlerAI>();
            }
            catch (System.Exception) { }
        }

        protected virtual void Start()
        {
            turnTime = baseTurnSpeed;
            targetPosition = startingPosition = transform.position;
            targetPosition += new Vector2(offset, 0);

        }

        protected virtual IEnumerator Co_MoveToAttack()
        {
            float elapsedTime = 0;

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.Lerp(startingPosition, targetPosition, elapsedTime);
                elapsedTime += (Time.deltaTime * attackAnimationSpeed);
                yield return null;
            }

            StartCoroutine(checkAI());
        }

        protected IEnumerator checkAI()
        {
            if (ai)
            {
                ICommand command = ai.ChooseAction();
                StartCoroutine(command.Co_Execute());

                while (!command.isFinished)
                {
                    yield return null;
                }
                //Battle command here
                StartCoroutine(Co_MoveToStarting());
            }
            else
                yield return null;
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





    }
}
