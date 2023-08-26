using System.Collections;
using UnityEngine;

namespace MGCNTN.Battle
{
    [System.Serializable]
    public class ActorGraphics
    {

        ///Public Parameters
        //Animation and movement
        [Header("Animation")]
        public GameObject selector;
        public float offset;
        public float animationSpeed;

        //Positions
        public Vector2 currPosition => actor.transform.position;
        public Animator anim { get; private set; }

        public bool attackSetupComplete = false;

        //Selector
        [Header("Selector")]
        [SerializeField] private Vector2 selectorPosition;
        private Vector2 startingPosition;
        private Vector2 targetPosition;
        private Actor actor;

        ///Public Functions
        //Constructor
        public void init(Actor actor, Animator animator)
        {
            this.actor = actor;
            anim = animator;

            if (animationSpeed == 0)
                animationSpeed = 3f;


            selector.transform.position = selectorPosition;
            selector = GameObject.Instantiate(selector, actor.transform);
            selector.SetActive(false);

            targetPosition = startingPosition = actor.transform.position;
            targetPosition += new Vector2(offset, 0);
        }

        ///Animations
        // Moves to middle
        public IEnumerator Co_MoveToAttack()
        {
            attackSetupComplete = false;

            if (anim)
                anim.Play("Moving");

            while ((Vector2)currPosition != targetPosition)
            {
                actor.transform.position = Vector2.MoveTowards(currPosition, targetPosition, Time.deltaTime * animationSpeed);

                yield return null;
            }

            if (anim)
                anim.Play("Idle");

            yield return new WaitForSeconds(.5f);

            attackSetupComplete = true;
        }

        //Moving back to middle waiting then back to starting
        public IEnumerator EndTurn()
        {

            if (anim)
                anim.Play("Moving");

            while (currPosition != targetPosition)
            {
                actor.transform.position = Vector2.MoveTowards(currPosition, targetPosition, Time.deltaTime * animationSpeed);
                yield return null;
            }

            if (anim)
            {
                anim.Play("Idle");
                yield return new WaitForSeconds(.5f);
                anim.Play("Moving");
            }

            while (currPosition != startingPosition)
            {
                actor.transform.position = Vector2.MoveTowards(currPosition, startingPosition, Time.deltaTime * animationSpeed);
                yield return null;
            }


            if (anim)
                anim.Play("Idle");
            actor.turner.isTakingTurn = false;
        }

        //Death Anim
        public IEnumerator CO_die()
        {
            if (anim != null)
            {
                anim.Play("Death");
                do yield return null;
                while (anim.IsAnimating());
                anim.StopPlayback();
            }

            actor.IsDead = true;
            yield return null;
        }
    }
}
