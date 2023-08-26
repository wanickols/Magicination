using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    [System.Serializable]
    public class EquippableStats : Stats
    {

        [SerializeField] private baseClasses[] requiredClass;
        [SerializeField] private ItemRarity rarity;

        public EquippableStats() : base() { }

        public EquippableStats(int lv, int exp, int hp, int maxHp, int mp, int maxMp, int atk, int matk, int def, int mdef, int spd, int evs) : base(lv, exp, hp, maxHp, mp, maxMp, atk, matk, def, mdef, spd, evs) { }

        public List<int> getDisplayStatValues()
        {
            List<int> results = new List<int>();

            results.Add(atk);
            results.Add(matk);
            results.Add(def);
            results.Add(mdef);
            results.Add(spd);
            results.Add(evs);

            return results;
        }

        public int getDisplayStatValue(int index)
        {
            return index switch
            {
                0 => atk,
                1 => matk,
                2 => def,
                3 => mdef,
                4 => spd,
                5 => evs,
                _ => 0
            };
        }
    }
}