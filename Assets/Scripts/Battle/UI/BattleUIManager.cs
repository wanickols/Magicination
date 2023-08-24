using Core;
using System;
using System.Collections;
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
        [SerializeField] private GameObject BattleUIContainer;
        [SerializeField] private GameObject gameOverPrefab;

        [Header("Selector Manager")]
        [SerializeField] private BattleSelection selectorManager = new BattleSelection();
        [SerializeField] private MenuInputHandler menuInputHandler = new MenuInputHandler();
        [SerializeField] private BattleWindow battleWindow;


        private BattleData data;
        private List<StatContainer> statsContainerList = new List<StatContainer>();
        private Selection selection = new Selection();


        /// Public
        public void init(BattleData data, Battle battle)
        {
            this.data = data;
            selectorManager.init(battleWindow, battle); ;
            menuInputHandler.Init(selectorManager);
        }

        public StatContainer AddPartyMemberUI(PartyMember member)
        {
            GameObject partyMem = GameObject.Instantiate(StatContainerPrefab, Stats.transform);

            statsContainerList.Add(partyMem.GetComponent<StatContainer>());
            StatContainer stat = statsContainerList.Last();

            stat.DisplayInfo(member);

            return stat;

        }

        public void update()
        {
            menuInputHandler.HandleInput();
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


        public void hideUI()
        {
            BattleUIContainer.SetActive(false);
        }

        public IEnumerator CO_GameOver()
        {
            Animator anim = GameObject.Instantiate(gameOverPrefab, BattleUIContainer.transform.parent).GetComponent<Animator>();
            while (anim.IsAnimating()) yield return null;

            Battle.quit?.Invoke();
        }

    }

}
