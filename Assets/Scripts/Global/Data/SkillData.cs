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
        public int Power = 0;

        [SerializeField] private SkillStatus status = SkillStatus.hidden;


        public SkillStatus skillStatus
        {
            get => status;
            set { status = value; changedStatus?.Invoke(); }

        }

        public static int nextID = 0;

        protected override void usePerm(MemberBattleInfo target, Stats user)
        {
            if (user.ENG < cost)
                return; //Call Invalid action noise or something

            user.ENG -= cost;

            base.usePerm(target, user);
        }

        protected override void useTemp(MemberBattleInfo target, Stats user)
        {
            if (user.ENG < cost)
                return; //Call Invalid action noise or something

            user.ENG -= cost;

            base.useTemp(target, user);
        }

        public SkillData()
        {
            nextID++;
            id = nextID;
        }
    }
}