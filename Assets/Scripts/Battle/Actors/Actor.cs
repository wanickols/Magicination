using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{

    public abstract class Actor : MonoBehaviour
    {


        //Events
        public Action<int, int> updateHealth;
        public Action<int, int> updateMP;
        public Action Death;

        public BattlerAI ai;

        //Store actor's stats and methods for taking a turn
        protected Vector2 startingPosition;
        protected Vector2 targetPosition;


        public Animator animator { get; private set; }

        //Battle Data
        public Stats Stats { get; set; }
        public Sprite battlePortrait { get; private set; }

        //Turn Related
        public float baseTurnSpeed => Stats.turnSpeed;
        public float turnTime = 0;
        public bool isDead { get; protected set; } = false;
        public bool isTakingTurn { get; protected set; } = false;

        //Selector
        [Header("Selector")]
        [SerializeField] public GameObject selector;
        [SerializeField] protected Vector2 selectorPosition;

        //Animation and movement
        [Header("Animation")]
        [SerializeField] protected float offset;
        [SerializeField] public float animationSpeed = 3f;

        ///Public
        public void setMemberBattleInfo(Stats stats, Sprite sprite)
        {
            this.Stats = stats;
            this.battlePortrait = sprite;
        }

        public virtual void StartTurn()
        {
            if (!isDead)
            {
                isTakingTurn = true;
                Battle.Attack += attack;
                StartCoroutine(Co_MoveToAttack());
            }
        }

        public IEnumerator CO_die()
        {
            Death.Invoke();
            isDead = true;
            if (animator)
            {
                animator.Play("Death");
                do
                {
                    yield return null;
                } while (animator.IsAnimating());
            }

            yield return null;
        }

        //Battle

        //Eventually will need more info, like which skill and such.
        protected virtual void attack(List<Actor> targets)
        {

            Battle.Attack -= attack;


            Attack command = new Attack(this, targets);

            StartCoroutine(ExecuteCommand(command));
        }

        /// Protected

        protected virtual void Awake()
        {
            try
            {
                animator = GetComponent<Animator>();
                ai = GetComponent<BattlerAI>();
            }
            catch (System.Exception) { }
        }

        protected virtual void Start()
        {
            selector.transform.position = selectorPosition;
            selector = Instantiate(selector, transform);
            selector.SetActive(false);
            turnTime = baseTurnSpeed;
            targetPosition = startingPosition = transform.position;
            targetPosition += new Vector2(offset, 0);
        }

        protected virtual IEnumerator Co_MoveToAttack()
        {
            if (animator)
                animator.Play("Moving");

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * animationSpeed);

                yield return null;
            }
            if (animator)
                animator.Play("Idle");

            yield return new WaitForSeconds(.5f);

            StartCoroutine(checkAI());
        }

        protected IEnumerator checkAI()
        {
            if (ai)
            {
                Battle.Attack -= attack;
                ICommand command = ai.ChooseAction();

                StartCoroutine(ExecuteCommand(command));
            }
            else
                yield return null;
        }

        protected virtual IEnumerator EndTurn()
        {


            if (animator)
                animator.Play("Moving");



            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * animationSpeed);

                yield return null;
            }
            if (animator)
                animator.Play("Idle");

            yield return new WaitForSeconds(.5f);

            if (animator)
                animator.Play("Moving");



            while ((Vector2)transform.position != startingPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, startingPosition, Time.deltaTime * animationSpeed);
                yield return null;
            }
            if (animator)
                animator.Play("Idle");
            isTakingTurn = false;
        }


        private IEnumerator ExecuteCommand(ICommand command)
        {
            StartCoroutine(command.Co_Execute());

            while (!command.isFinished)
            {
                yield return null;
            }
            //Battle command here
            StartCoroutine(EndTurn());
        }


        private void OnDestroy()
        {
            Battle.Attack -= attack;
        }

    }
}
