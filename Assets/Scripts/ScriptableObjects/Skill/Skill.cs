using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    [CreateAssetMenu(fileName = "Skill", menuName = ("Skill"))]
    public class Skill : ScriptableObject
    {
        public Action finished;

        [SerializeField] GameObject skillPrefab;
        [SerializeField] List<AnimationClip> animationClips = new List<AnimationClip>(3);
        [SerializeField] private SkillData data = new SkillData();

        public SkillData Data
        {
            get => data;
            set => data = value;
        }

        public void Spawn(Transform position, Vector3 target)
        {
            SkillAnimation animer = Instantiate(skillPrefab, position).GetComponent<SkillAnimation>();
            animer.ReplaceAnimations(animationClips);
            animer.StartAnimation(target);
            animer.finishedAnimation += finished;
        }
    }
}