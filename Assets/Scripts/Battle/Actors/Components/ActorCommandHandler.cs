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

            if (effectsHandler != null)
            {
                effectsHandler.Tick();

                if (!effectsHandler.canMove)
                    return;
            }

            actor.isTakingTurn = true;
            StartCoroutine(gfx.Co_MoveToAttackAnim());
            StartCoroutine(checkAI());
        }


        //Battle Choices
        public bool attack(List<Actor> targets)
        {
            Attack command = new Attack(actor, targets);
            executeCommand(command);

            return true;
        }
        public bool use(List<Actor> targets, ObjectData data)
        {
            if (!checkSkill(data))
                return false;

            Use command = new Use(actor, targets, data);
            executeCommand(command);

            return true;
        }

        public bool checkSkill(ObjectData data)
        {
            if (data is SkillData)
                return ((SkillData)data).cost < actor.Stats.ENG;

            return true;
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