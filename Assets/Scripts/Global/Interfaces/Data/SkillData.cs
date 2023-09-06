using System;
using UnityEngine;

namespace MGCNTN
{

    [Serializable]
    public class SkillData : ObjectData
    {
        //Actions
        public Action changedStatus;

        [Header("Skills")]

        public int cost;
        public bool menuUsable;

        [SerializeField] private SkillStatus status = SkillStatus.hidden;


        public SkillStatus skillStatus
        {
            get => status;
            set { status = value; changedStatus?.Invoke(); }

        }

        public static int nextID = 0;

        public override void use(Stats target, Stats user) => augData.CreateAugmentation(target, user, true).ApplyEffect();


        public SkillData()
        {
            nextID++;
            id = nextID;
        }
    }
}
