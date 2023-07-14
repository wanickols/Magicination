using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class EnemyStats : Stats
    {



        [SerializeField] protected int battleWeight;
        [SerializeField] public EnemyRarity enemyRarity = EnemyRarity.common;

        public EnemyStats(int lv, int exp, int hp, int maxHp, int mp, int maxMp, int atk, int matk, int def, int mdef, int spd, int evs, int battleWeight) : base(lv, exp, hp, maxHp, mp, maxMp, atk, matk, def, mdef, spd, evs)
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
