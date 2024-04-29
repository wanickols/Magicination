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


            Use command = new Use(actor, targets, data);
            executeCommand(command);

            return true;
        }

        public bool useSkill(List<Actor> targets, Skill skill)
        {
            if (!checkSkill(skill.Data))
                return false;


            UseSkill command = new UseSkill(actor, targets, skill);
            executeCommand(command);

            return true;
        }



        /// Private Functions

        private bool checkSkill(SkillData data) => data.cost < actor.Stats.ENG;
        private void executeCommand(ICommand command) => StartCoroutine(CO_ExecuteCommand(command));

        private IEnumerator CO_ExecuteCommand(ICommand command)
        {
            StartCoroutine(command.Co_Execute());

            while (!command.isFinished)
                yield return null;

            actor.UpdateEnergy();
            actor.UpdateHealth();

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