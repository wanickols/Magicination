using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    [System.Serializable]
    public class EquippableStats : Stats
    {

        [SerializeField] private BaseClasses[] requiredClass;
        [SerializeField] private ItemRarity rarity;

        public EquippableStats() : base() { }

        public EquippableStats(int lv, int exp, int hp, int maxHp, int eng, int maxEng, int atk, int def, int matk, int mdef, int spd, int evs) : base(lv, exp, hp, maxHp, eng, maxEng, atk, def, matk, mdef, spd, evs) { }

        public List<int> getDisplayStatValues()
        {
            List<int> results = new List<int>();

            results.Add(hp);
            results.Add(eng);
            results.Add(atk);
            results.Add(def);
            results.Add(matk);
            results.Add(mdef);
            results.Add(spd);
            results.Add(evs);

            return results;
        }

        public int getDisplayStatValue(int index)
        {
            return index switch
            {
                0 => hp,
                1 => eng,
                2 => atk,
                3 => def,
                4 => matk,
                5 => mdef,
                6 => spd,
                7 => evs,
                _ => 0
            };
        }
    }
}