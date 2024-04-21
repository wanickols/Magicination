using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class ObjectData
    {
        [Header("Basics")]
        public int id;
        public string displayName;
        public string description;
        public int requiredLevel;
        public int duration = 0;
        public Sprite sprite;

        [Header("Augmentation")]
        public AugmentationData augData;

        public void use(MemberBattleInfo target, Stats user)
        {
            if (duration == 0)
                usePerm(target, user);
            else
                useTemp(target, user);
        }

        protected virtual void usePerm(MemberBattleInfo target, Stats user) => augData.CreateAugmentation(target).ApplyEffect();

        protected virtual void useTemp(MemberBattleInfo target, Stats user) => augData.CreateAugmentation(target, duration, id).ApplyEffect();



    }
}