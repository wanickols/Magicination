using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class SkillAnimation : MonoBehaviour
    {
        public Action finishedAnimation;

        public float projectileSpeed = 3;


        [SerializeField] Animator animator;

        private AnimatorOverrideController overrideController;
        private Vector3 targetPosition;



        public void ReplaceAnimations(List<AnimationClip> clips)
        {
            if (clips.Count < 3)
                return;

            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            replaceClip(clips[0], "Start");
            replaceClip(clips[1], "Idle");
            replaceClip(clips[2], "End");

            animator.runtimeAnimatorController = overrideController;
        }



        // Call this method to start the attack animation and move towards the target
        public void StartAnimation(Vector3 target)
        {
            targetPosition = target;
            StartCoroutine(CO_StartAnimation());
        }


        private IEnumerator CO_StartAnimation()
        {
            animator.Play("Start");

            yield return null;

            while (transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * projectileSpeed);
                yield return null;
            }

            StartCoroutine(CO_EndAnimation());
        }
        private IEnumerator CO_EndAnimation()
        {
            animator.Play("End");
            finishedAnimation?.Invoke();

            do yield return null;
            while (animator.IsAnimating());


            Destroy(this);
        }
        private void replaceClip(AnimationClip clip, string stateName) => overrideController[stateName] = clip;
    }
}