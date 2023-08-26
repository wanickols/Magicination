using System.Collections;
using UnityEngine;

namespace MGCNTN.Battle
{
    [System.Serializable]
    public class ActorGraphics
    {
        //Selector
        [SerializeField] protected Vector2 selectorPosition;

        //Animation and movement
        [Header("Animation")]
        [SerializeField] public GameObject selector;
        [SerializeField] public float offset;
        [SerializeField] public float animationSpeed;

        //Positions
        public Vector2 currPosition => actor.transform.position;

        protected Vector2 startingPosition;
        protected Vector2 targetPosition;



        public Animator animator { get; private set; }
        private Actor actor;

        //Constructor
        public void init(Actor actor, Animator animator)
        {
            this.actor = actor;

            if (animator)
                this.animator = animator;

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
        public virtual IEnumerator Co_MoveToAttack()
        {
            if (animator)
                animator.Play("Moving");

            while ((Vector2)currPosition != targetPosition)
            {
                actor.transform.position = Vector2.MoveTowards(currPosition, targetPosition, Time.deltaTime * animationSpeed);

                yield return null;
            }
            if (animator)
                animator.Play("Idle");

            yield return new WaitForSeconds(.5f);

            actor.startCheckAI();
        }

        //Moving back to middle waiting then back to starting
        public virtual IEnumerator EndTurn()
        {


            if (animator)
                animator.Play("Moving");



            while ((Vector2)currPosition != targetPosition)
            {
                actor.transform.position = Vector2.MoveTowards(currPosition, targetPosition, Time.deltaTime * animationSpeed);

                yield return null;
            }
            if (animator)
                animator.Play("Idle");

            yield return new WaitForSeconds(.5f);

            if (animator)
                animator.Play("Moving");



            while ((Vector2)currPosition != startingPosition)
            {
                actor.transform.position = Vector2.MoveTowards(currPosition, startingPosition, Time.deltaTime * animationSpeed);
                yield return null;
            }
            if (animator)
                animator.Play("Idle");

            actor.setIsTakingTurn(false);
        }

        //Death Anim
        public IEnumerator CO_die()
        {
            if (animator)
            {
                animator.Play("Death");
                do
                {
                    yield return null;
                } while (animator.IsAnimating());
                animator.StopPlayback();
            }

            actor.setDead(true);
            yield return null;
        }
    }
}
