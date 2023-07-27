using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class BattleUIManager
    {
        [Header("UI")]
        [SerializeField] private GameObject Stats;
        [SerializeField] private GameObject StatContainerPrefab;
        [SerializeField] private GameObject BattleMenu;

        private BattleData data;
        private List<StatContainer> statsContainerList = new List<StatContainer>();

        /// Public
        public void initData(BattleData data) => this.data = data;

        public StatContainer AddPartyMemberUI(PartyMember member)
        {
            GameObject partyMem = GameObject.Instantiate(StatContainerPrefab, Stats.transform);

            statsContainerList.Add(partyMem.GetComponent<StatContainer>());
            StatContainer stat = statsContainerList.Last();

            stat.DisplayInfo(member);

            return stat;

        }

        public void LinkListeners(Actor actor)
        {
            StatContainer stat = statsContainerList.Last();
            actor.updateHealth += stat.updateHealth;
            actor.updateMP += stat.updateMP;
        }

        public void toggleBattleMenu()
        {
            if (data.currentActor.GetComponent<BattlerAI>())
            {
                setBattleMenu(false);
            }
            else
                setBattleMenu(true);
        }
        public void setBattleMenu(bool active)
        {
            BattleMenu.SetActive(active);
        }
    }

}
