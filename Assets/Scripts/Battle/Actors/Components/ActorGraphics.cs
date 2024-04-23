using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    [System.Serializable]
    public class ActorGraphics
    {

        ///Public Parameters

        [Header("UI Elements")]
        public GameObject statusContainer;
        private Animator statusAnimator;

        [Space(4)]
        public GameObject selector;

        //Selector
        [SerializeField] private Vector2 selectorPosition;
        private Vector2 startingPosition;
        private Vector2 targetPosition;
        private Actor actor;

        //Animation and movement
        [Header("Animation")]
        public float offset;
        public float animationSpeed;
        public int statusWaitTimeMil = 600;
        private int currStatusTime;

        //Positions
        public Vector2 currPosition => actor.transform.position;
        public Animator anim { get; private set; }

        public bool attackSetupComplete = false;



        ///Public Functions
        //Constructor
        public void init(Actor actor, Animator animator)
        {
            this.actor = actor;
            anim = animator;

            if (animationSpeed == 0)
                animationSpeed = 3f;

            if (statusWaitTimeMil == 0)
                statusWaitTimeMil = 300;

            currStatusTime = statusWaitTimeMil;

            selector.transform.position = selectorPosition;
            selector = GameObject.Instantiate(selector, actor.transform);
            selector.SetActive(false);

            statusContainer = GameObject.Instantiate(statusContainer, actor.transform);
            statusContainer.SetActive(false);
            statusAnimator = statusContainer.GetComponent<Animator>();

            targetPosition = startingPosition = actor.transform.position;
            targetPosition += new Vector2(offset, 0);
        }


        private StatusType currentStatus;
        private Queue<Status> statusQueue = new Queue<Status>();
        private bool isPlayingStatus = false;

        public void AnimateStatuses()
        {

            if (++currStatusTime < statusWaitTimeMil)
                return;

            currStatusTime = 0;

            if (statusQueue.Count > 0)
                currentStatus = statusQueue.Peek().type;
            else
            {
                if (isPlayingStatus)
                {
                    statusAnimator.StopPlayback();
                    statusContainer.SetActive(false);
                }
                return;
            }

            string statusName = string.Empty;
            switch (currentStatus)
            {
                case StatusType.Burn:
                    statusName = "Burn";
                    break;
                case StatusType.Poison:
                    statusName = "Poison";
                    break;
                default:
                    break;
            }


            if (statusName != string.Empty && !isPlayingStatus)
            {
                statusContainer.SetActive(true);
                statusAnimator.Play(statusName);
                isPlayingStatus = true;
            }
            else
            {
                isPlayingStatus = false;
                statusAnimator.StopPlayback();
                statusContainer.SetActive(false);
            }


            statusQueue.Enqueue(statusQueue.Dequeue());
        }



        public void RefreshStatus()
        {
            List<Status> statusList = actor.memberBattleInfo.Statuses.statusList;

            if (statusList.Count == statusQueue.Count)
                return;

            statusList.Sort((s1, s2) => s2.duration.CompareTo(s1.duration));
            statusQueue.Clear();
            foreach (Status status in statusList)
            {
                statusQueue.Enqueue(status);
            }
        }

        ///Animations
        // Moves to middle
        public IEnumerator Co_MoveToAttackAnim()
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
        public IEnumerator EndTurnAnim()
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

            actor.isTakingTurn = false; ///Maybe reconsider using an an action
        }

        //Death Anim
        public IEnumerator CO_DeathAnim()
        {
            if (anim != null)
            {
                anim.Play("Death");
                do yield return null;
                while (anim.IsAnimating());
                anim.StopPlayback();
            }

            actor.IsDead = true;
        }

        //Attack Anim
        public IEnumerator CO_AttackAnim(Actor target)
        {
            //Init
            Vector3 targetPos = target.transform.position;
            Vector3 offset = new Vector3(.5f, 0, 0);

            if (target.type == ActorType.Enemy)
                offset.x = -.6f;

            targetPos = targetPos + offset;

            //Start
            if (anim)
                anim.Play("Moving");

            //Move
            while (currPosition != (Vector2)targetPos)
            {
                actor.transform.position
                    = Vector2.MoveTowards(currPosition,
                    targetPos, animationSpeed * Time.deltaTime);

                yield return null;
            }


            //Atack
            if (anim)
            {
                anim.Play("Attack");

                do yield return null;
                while (anim.IsAnimating());

                anim.Play("Idle");
            }
        }

        public IEnumerator CO_HitAnim(int damage)
        {
            if (!anim)
                yield return null;
            else
            {
                anim.Play("Hit");


                // Do a little damage thingy

                do yield return null;
                while (anim.IsAnimating());

                anim.Play("Idle");
            }
        }

        //Item Use
        public IEnumerator CO_ItemAnim(Actor target)
        {
            Debug.Log("Implement Item Animation");
            yield return null;
        }


    }
}
