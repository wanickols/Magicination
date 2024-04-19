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
        public int maxStacks = 1;
        private int stackCount = 0;
        public Sprite sprite;

        [Header("Augmentation")]
        public AugmentationData augData;

        public virtual void usePerm(MemberBattleInfo target, Stats user)
        {
            if (canStack())
                augData.CreateAugmentation(target).ApplyEffect();
        }

        public virtual void useTemp(MemberBattleInfo target, Stats user)
        {
            if (canStack())
                augData.CreateAugmentation(target, duration, id).ApplyEffect();
        }

        public void decrementStack()
        {
            if (--maxStacks < 0)
                maxStacks = 0;
        }
        protected bool canStack()
        {

            if (++stackCount >= maxStacks)
            {
                stackCount = maxStacks;
                return false; //invalid noise
            }

            return true;
        }


    }
}