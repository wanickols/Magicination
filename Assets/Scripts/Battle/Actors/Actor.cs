using System;
using UnityEngine;

namespace MGCNTN.Battle
{

    public class Actor : MonoBehaviour
    {


        ///Events
        public Action<int, int> updateHealth;
        public Action<int, int> updateMP;
        public Action Death;


        ///Public Parameters
        //Components
        public CommandHandler commander { get; private set; }
        public BattlerAI ai;

        //Data Accessors

        ///Public Paramaeters
        [NonSerialized] public float turnTime = 0;

        [NonSerialized] public bool isTakingTurn = false;
        //Battle Data
        public MemberBattleInfo memberBattleInfo { get; private set; }
        public Stats Stats => memberBattleInfo.Stats;

        public float baseTurnSpeed => Stats.turnSpeed;
        public Sprite battlePortrait => memberBattleInfo.MenuPortrait;

        //Type
        public ActorType type;
        [Header("Graphics")]
        public ActorGraphics gfx = new ActorGraphics();

        ///Private Parameters
        //Components

        //Variables
        private bool isDead = false;

        // Dead
        public bool IsDead
        {
            get => isDead;
            set
            {
                isDead = value;
                if (isDead)
                    Death?.Invoke();
            }
        }

        /// Unity Functions
        private void Awake()
        {
            ai = GetComponent<BattlerAI>();
            commander = GetComponent<CommandHandler>();
            Animator animator = GetComponent<Animator>();
            gfx.init(this, animator);
        }

        ///Public Functions
        public void setMemberBattleInfo(MemberBattleInfo info)
        {
            memberBattleInfo = info;
            turnTime = baseTurnSpeed;
        }
        public void Die() => StartCoroutine(gfx.CO_DeathAnim());

        public void checkDeath(bool instant)
        {
            if (Stats.HP <= 0)
            {
                Stats.HP = 0;

                if (instant)
                    IsDead = true;

                Die();
            }
        }

    }
}
