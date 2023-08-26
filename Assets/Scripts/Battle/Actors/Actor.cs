using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{

    public abstract class Actor : MonoBehaviour
    {


        /// Actions
        public Action<int, int> updateHealth;
        public Action<int, int> updateMP;
        public Action Death;

        /// Components
        public BattlerAI ai;

        [Header("Graphics")]
        [SerializeField] public ActorGraphics gfx = new ActorGraphics();


        //Battle Data
        public Stats Stats { get; set; }
        public Sprite battlePortrait { get; private set; }

        //Turn Related
        public float baseTurnSpeed => Stats.turnSpeed;
        public float turnTime = 0;
        public bool isTakingTurn { get; protected set; } = false;

        // Dead
        public bool isDead = false;

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
                Battle.UseItem += useItem;
                StartCoroutine(gfx.Co_MoveToAttack());
            }

        }

        public virtual void Die()
        {
            StartCoroutine(gfx.CO_die());
        }

        public void setDead(bool dead)
        {
            if (!isDead)
            {

                isDead = dead;

                if (dead)
                    Death?.Invoke();
            }
        }
        public void startCheckAI()
        {
            StartCoroutine(checkAI());
        }
        public void setIsTakingTurn(bool takingTurn)
        {
            isTakingTurn = takingTurn;
        }


        /// Unity Functions
        protected virtual void Awake()
        {
            ai = GetComponent<BattlerAI>();
            Animator animator = GetComponent<Animator>();
            gfx.init(this, animator);
        }
        protected virtual void Start()
        {
            turnTime = baseTurnSpeed;
        }

        /// Protected Functions

        protected virtual void attack(List<Actor> targets)
        {
            unlinkBattle();
            Attack command = new Attack(this, targets);
            StartCoroutine(ExecuteCommand(command));
        }
        protected virtual void useItem(List<Actor> targets, IConsumable item)
        {

            unlinkBattle();
            UseItem command = new UseItem(targets, item);

            StartCoroutine(ExecuteCommand(command));
        }

        //Battle Choice
        protected IEnumerator checkAI()
        {
            if (ai)
            {
                unlinkBattle();
                ICommand command = ai.ChooseAction();

                StartCoroutine(ExecuteCommand(command));
            }
            else
                yield return null;
        }

        protected IEnumerator ExecuteCommand(ICommand command)
        {
            StartCoroutine(command.Co_Execute());

            while (!command.isFinished)
            {
                yield return null;
            }
            //Battle command here
            StartCoroutine(gfx.EndTurn());
        }

        //Deconstructor
        protected void OnDestroy()
        {
            unlinkBattle();
        }

        private void unlinkBattle()
        {
            Battle.Attack -= attack;
            Battle.UseItem -= useItem;
        }

    }
}
