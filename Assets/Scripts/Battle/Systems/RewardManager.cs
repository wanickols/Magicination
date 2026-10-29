using MGCNTN.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MGCNTN.Battle
{
    public class RewardManager : MonoBehaviour
    {

        private Dictionary<int, int> levelExp = new();

        public void AwardExp(int level, int val)
        {
            levelExp.TryGetValue(level, out int currentExp);
            levelExp[level] = currentExp + val;
        }
        public void DetermineRewards(List<Actor> allies)
        {
            foreach (Actor ally in allies)
            {
                Stats s = ally.memberBattleInfo.Stats;
                int xp = calculateExp(s);
                s.EXP += xp;
            }
        }

        private int calculateExp(Stats ally)
        {
            int totalExp = 0;

            foreach (KeyValuePair<int, int> enemy in levelExp)
            {
                int levelDifference = enemy.Key - ally.LV;
                int baseExp = enemy.Value;

                totalExp += calculateExpReward(baseExp, levelDifference);
            }

            return totalExp;
        }

        private int calculateExpReward(int baseExp, int levelDifference)
        {
            float multiplier;

            if (levelDifference >= 5)
                multiplier = 1.75f;
            else if (levelDifference >= 3)
                multiplier = 1.45f;
            else if (levelDifference >= 1)
                multiplier = 1.2f;
            else if (levelDifference >= -1)
                multiplier = 1.0f;
            else if (levelDifference >= -3)
                multiplier = 0.55f;
            else if (levelDifference >= -5)
                multiplier = 0.25f;
            else
                multiplier = 0.1f;

            return (int)(baseExp * multiplier);
        }
    }
}