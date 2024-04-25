using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class EffectsHandler
    {


        const float poisonPercentage = 0.05f; // 5% poison damage


        ///Actions
        public Action statusChanged;
        public Action<int> damageApplied;
        public Action<int, Color> coloredDamage;

        ///Public Variables
        public bool canMove = true;
        public bool ticking = false;
        ///Private variables
        private MemberBattleInfo m_battleInfo;

        private StatusCollection statuses => m_battleInfo.Statuses;
        private List<AugmentStats> augmentStats => m_battleInfo.augmentingStats;

        ///Public Functions
        public EffectsHandler(MemberBattleInfo battleInfo)
        {
            m_battleInfo = battleInfo;
        }

        public void Tick()
        {
            canMove = true;

            tickStatuses();
            tickStats();
        }

        public void removeStatus(Status status)
        {
            statuses.Remove(status.type);
            statusChanged?.Invoke();
        }
        public void clearStatuses()
        {
            statuses.clear();
            statusChanged?.Invoke();
        }

        ///Private Functions
        private void tickStatuses()
        {
            foreach (Status status in statuses.statusList)
                applyStatus(status);

            statuses.tickDuration();

            statusChanged?.Invoke();
        }

        private void applyStatus(Status status)
        {
            switch (status.type)
            {
                case StatusType.Burn:
                    applyBurn(status);
                    break;
                case StatusType.Poison:
                    applyPoison(status);
                    break;
                case StatusType.Paralysis:
                case StatusType.Petrified:
                case StatusType.Sleep:
                    canMove = false;
                    break;
                default:
                    break;
            }
        }

        private void applyPoison(Status status)
        {

            int maxHP = m_battleInfo.baseStats.MAXHP;
            int poisonDamage = Mathf.RoundToInt(maxHP * poisonPercentage * status.severityLevel);

            // Apply poison damage to current HP
            m_battleInfo.baseStats.HP -= poisonDamage;
            coloredDamage?.Invoke(poisonDamage, Color.magenta);
        }

        private void applyBurn(Status status)
        {
            int burnDamage = 10 * status.severityLevel;

            m_battleInfo.baseStats.HP -= burnDamage;
            coloredDamage?.Invoke(burnDamage, Color.red);
        }
        private void tickStats() => augmentStats.tickDuration();

    }

}
