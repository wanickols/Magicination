using Battle;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class EquippableStats : Stats
    {

        [SerializeField] private baseClasses[] requiredClass;
        [SerializeField] private ItemRarity rarity;

        public EquippableStats(int lv, int exp, int hp, int maxHp, int mp, int maxMp, int atk, int matk, int def, int mdef, int spd, int evs) : base(lv, exp, hp, maxHp, mp, maxMp, atk, matk, def, mdef, spd, evs)
        {
        }
    }
}