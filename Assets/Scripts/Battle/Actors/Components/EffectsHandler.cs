using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class EffectsHandler
    {
        const float poisonPercentage = 0.05f; // 5% poison damage

        ///Public Variables
        public bool canMove = true;

        ///Private variables
        private MemberBattleInfo m_battleInfo;

        private List<Status> statuses => m_battleInfo.Statuses;
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

        ///Private Functions
        private void tickStatuses()
        {
            //Apply Status

            foreach (Status status in statuses)
                applyStatus(status);

            m_battleInfo.Statuses.tickDuration();
        }

        private void applyStatus(Status status)
        {
            switch (status.type)
            {
                case StatusType.Burn:

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
        }

        private void tickStats() => augmentStats.tickDuration();

    }

}
