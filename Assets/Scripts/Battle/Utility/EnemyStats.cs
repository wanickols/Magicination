using UnityEngine;

namespace MGCNTN.Battle
{
    [System.Serializable]
    public class EnemyStats : Stats
    {



        [SerializeField] protected int battleWeight;
        [SerializeField] public EnemyRarity enemyRarity = EnemyRarity.common;

        public EnemyStats(int lv, int exp, int hp, int maxHp, int eng, int maxEng, int atk, int def, int matk, int mdef, int spd, int evs, int battleWeight) : base(lv, exp, hp, maxHp, eng, maxEng, atk, def, matk, mdef, spd, evs)
        {
            this.battleWeight = battleWeight;
        }

        protected const int MAX_BATTLE_WEIGHT = 10;


        public int BattleWeight
        {
            get => battleWeight;
            set
            {
                battleWeight = Mathf.Clamp(value, 0, MAX_BATTLE_WEIGHT);
            }
        }
    }
}
