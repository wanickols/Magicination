using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class ActorCommandHandler : MonoBehaviour
    {
        ///Private Parameters
        //Components
        private ActorAI ai => actor.ai;
        private ActorGraphics gfx => actor.gfx;
        private EffectsHandler effectsHandler => actor.effects;
        private Actor actor;

        ///Unity Functions        
        public void Awake() => actor = GetComponent<Actor>();

        ///Public functions
        public void startCheckAI() => StartCoroutine(checkAI());

        //Start Turn Logic
        public virtual void StartTurn()
        {
            if (actor.IsDead)
                return;

            effectsHandler.Tick();

            if (!effectsHandler.canMove)
                return;

            actor.isTakingTurn = true;
            StartCoroutine(gfx.Co_MoveToAttackAnim());
            StartCoroutine(checkAI());
        }


        //Battle Choices
        public void attack(List<Actor> targets)
        {
            Attack command = new Attack(actor, targets);
            executeCommand(command);
        }
        public void useItem(Actor user, List<Actor> targets, ObjectData data)
        {
            Use command = new Use(user, targets, data);
            executeCommand(command);
        }

        /// Private Functions
        private void executeCommand(ICommand command) => StartCoroutine(CO_ExecuteCommand(command));

        private IEnumerator CO_ExecuteCommand(ICommand command)
        {
            StartCoroutine(command.Co_Execute());

            while (!command.isFinished)
                yield return null;

            //Battle command here
            StartCoroutine(gfx.EndTurnAnim());
        }

        //AI Logic
        private IEnumerator checkAI()
        {
            while (!gfx.attackSetupComplete)
                yield return null;

            if (ai)
                executeCommand(ai.ChooseAction());
        }
    }
}