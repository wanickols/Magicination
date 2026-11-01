using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class ProgressionManager
    {
        private enum GrowthStat
        {
            ATK,
            MATK,
            DEF,
            MDEF,
            SPD,
            EVS
        }


        private static readonly System.Random random = new System.Random();

        //Arbitraty just getting this working for now. 
        public void AllocateStats(Stats stats)
        {
            // HP increases every level.
            stats.MAXHP += 5;

            // Weighted stat selection.
            List<GrowthStat> available = new List<GrowthStat>
    {
        GrowthStat.ATK,
        GrowthStat.ATK,
        GrowthStat.ATK,

        GrowthStat.MATK,
        GrowthStat.MATK,
        GrowthStat.MATK,

        GrowthStat.DEF,
        GrowthStat.DEF,
        GrowthStat.DEF,

        GrowthStat.MDEF,
        GrowthStat.MDEF,
        GrowthStat.MDEF,

        GrowthStat.SPD,
        GrowthStat.EVS
    };

            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(available.Count);
                GrowthStat selected = available[index];

                available.RemoveAll(stat => stat == selected);

                switch (selected)
                {
                    case GrowthStat.ATK:
                        stats.ATK++;
                        break;
                    case GrowthStat.MATK:
                        stats.MATK++;
                        break;
                    case GrowthStat.DEF:
                        stats.DEF++;
                        break;
                    case GrowthStat.MDEF:
                        stats.MDEF++;
                        break;
                    case GrowthStat.SPD:
                        stats.SPD++;
                        break;
                    case GrowthStat.EVS:
                        stats.EVS++;
                        break;
                }
            }

            //Reset
            stats.DamageTaken = stats.EnergyUsed = 0;

        }
    }
}