using System;
using UnityEngine;

namespace MGCNTN.Battle
{

    public class Actor : MonoBehaviour
    {


        ///Events
        public Action<int, int> updateHealth;
        public Action<int, int> updateEnergy;
        public Action Death;


        ///Public Parameters
        //Components
        public ActorCommandHandler commander { get; private set; }
        public EffectsHandler effects;
        public ActorAI ai;

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
        public ActorGraphics gfx;

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
            ai = GetComponent<ActorAI>();
            commander = GetComponent<ActorCommandHandler>();
            Animator animator = GetComponent<Animator>();

            gfx.init(this, animator);
        }

        private void Update() => gfx.AnimateStatuses();

        ///Public Functions
        public void setMemberBattleInfo(MemberBattleInfo info)
        {
            memberBattleInfo = info;
            info.updateStats();
            turnTime = baseTurnSpeed;
            effects = new EffectsHandler(memberBattleInfo);
            effects.statusChanged += statusApplied;
            effects.damageApplied += takeDamage;
            effects.coloredDamage += takeDamage;
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

        public void statusApplied() => gfx.RefreshStatus();
        public void takeDamage(int damage, Color color)
        {
            StartCoroutine(gfx.CO_DamageAnimation(damage, color));
            handleDamage(damage);
        }

        public void takeDamage(int damage)
        {
            StartCoroutine(gfx.CO_DamageAnimation(damage, Color.white));
            handleDamage(damage);
        }

        public void UpdateHealth()
        {
            updateHealth?.Invoke(Stats.HP, Stats.MAXHP);
        }

        public void UpdateEnergy()
        {
            updateEnergy?.Invoke(Stats.ENG, Stats.MAXENG);
        }

        ///Privat Functions
        private void handleDamage(int damage)
        {
            Stats.HP -= damage;
            updateHealth?.Invoke(Stats.HP, Stats.MAXHP);
            checkDeath(true);
        }

        ///Destroy
        private void OnDestroy()
        {
            if (effects != null)
            {
                effects.statusChanged -= statusApplied;
                effects.damageApplied -= takeDamage;
            }
        }
    }
}
