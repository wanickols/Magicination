using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class ActorTurner : MonoBehaviour
    {

        ///Public Paramaeters
        [NonSerialized] public float turnTime = 0;
        [NonSerialized] public bool isTakingTurn = false;

        ///Private Parameters
        //Components
        private BattlerAI ai => actor.ai;
        private ActorGraphics gfx => actor.gfx;
        private Actor actor;

        ///Unity Functions        
        public void Awake() => actor = GetComponent<Actor>();

        ///Public functions
        public void startCheckAI() => StartCoroutine(checkAI());
        public virtual void StartTurn()
        {
            if (actor.IsDead)
                return;

            isTakingTurn = true;
            linkBattle();
            StartCoroutine(gfx.Co_MoveToAttack());
            StartCoroutine(checkAI());
        }

        /// Private Functions
        //Battle Choices
        private void attack(List<Actor> targets)
        {
            Attack command = new Attack(actor, targets);
            ExecuteCommand(command);
        }
        private void useItem(List<Actor> targets, IConsumable item)
        {
            UseItem command = new UseItem(targets, item);
            ExecuteCommand(command);
        }

        private void ExecuteCommand(ICommand command)
        {
            unlinkBattle();
            StartCoroutine(CO_ExecuteCommand(command));
        }

        private IEnumerator CO_ExecuteCommand(ICommand command)
        {
            StartCoroutine(command.Co_Execute());

            while (!command.isFinished)
                yield return null;

            //Battle command here
            StartCoroutine(gfx.EndTurn());
        }

        //AI Logic
        private IEnumerator checkAI()
        {
            while (!gfx.attackSetupComplete)
                yield return null;

            if (ai)
                ExecuteCommand(ai.ChooseAction());
        }

        //Events
        private void linkBattle()
        {
            Battle.Attack += attack;
            Battle.UseItem += useItem;
        }
        private void unlinkBattle()
        {
            Battle.Attack -= attack;
            Battle.UseItem -= useItem;
        }

        ~ActorTurner() => unlinkBattle();
    }
}