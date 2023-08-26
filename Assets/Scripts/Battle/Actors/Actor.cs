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
        public ActorTurner turner { get; private set; }
        public BattlerAI ai;

        //Data Accessors
        public float baseTurnSpeed => data.Stats.turnSpeed;

        //Type
        public ActorType type { get; private set; }

        [Header("Graphics")]
        public ActorGraphics gfx = new ActorGraphics();

        ///Private Parameters
        //Components
        public ActorData data { get; private set; } = new ActorData();

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
            turner = GetComponent<ActorTurner>();
            Animator animator = GetComponent<Animator>();
            gfx.init(this, animator);
        }

        ///Public Functions
        public void setMemberBattleInfo(Stats stats, Sprite sprite)
        {
            data.Stats = stats;
            data.battlePortrait = sprite;
            turner.turnTime = baseTurnSpeed;
        }
        public void Die() => StartCoroutine(gfx.CO_die());

    }
}
