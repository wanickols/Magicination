using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGCNTN.Battle
{
    [Serializable]
    public class BattleUIManager
    {
        ///Events
        public Action run;
        public Action Invalid;

        ///Public Parameters
        public BattleMainSelections currBattleSelection => selectorManager.currBattleSelection;
        public Selection selection { get; private set; } = new Selection();

        /// Private Parameters
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
        private Battle battle;
        private List<StatContainer> statsContainerList = new List<StatContainer>();
        private List<Actor> actors = new List<Actor>();


        ///Unity Functions (sort of)
        public void update() => menuInputHandler.HandleInput();

        /// Public Functions
        //Init
        public void init(BattleData data, Battle battle)
        {
            this.data = data;
            this.battle = battle;
            Invalid += invalidSelection;
            selectorManager.init(battleWindow, this); ;
            menuInputHandler.Init(selectorManager);
            selection.cancelTarget += selectorManager.revertSelection;
        }

        //Battle Menu Functions
        public void setBattleMenu(bool active) => BattleMenu.SetActive(active);

        public void nextTurn()
        {
            setBattleMenu(data.currentActor.GetComponent<ActorAI>() == null);
            battleWindow.UpdateSkills();
        }

        public void hideUI() => BattleUIContainer.SetActive(false);

        public void revertToMain() => selectorManager.returnToMain(false);

        public void inputAllowed() => battle.inputAllowed = true;

        //Selection
        public void trySelect()
        {
            if (Battle.battleState == BattleStates.battle)
            {
                Battle.battleState = BattleStates.select;
                CO.startCO(selection.CO_SelectSingleTarget(data));
                setBattleMenu(false);
            }
        }

        //Battle Back and Forth
        public IEnumerator CO_GameOver()
        {
            Animator anim = GameObject.Instantiate(gameOverPrefab, BattleUIContainer.transform.parent).GetComponent<Animator>();
            while (anim.IsAnimating()) yield return null;
            Battle.quit?.Invoke();
        }

        public void tryRun() => run?.Invoke();

        //Items
        public Consumable getConsumable() => selectorManager.currConsumable;
        public Skill getSKill() => selectorManager.currSkill;



        //Party Member Generation
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
            actors.Add(actor);
            StatContainer stat = statsContainerList.Last();
            actor.updateHealth += stat.updateHealth;
            actor.updateEnergy += stat.updateMP;


        }
        ///Private Functions
        ~BattleUIManager() => UnLinkListeners();

        private void UnLinkListeners()
        {
            int i = 0;
            foreach (Actor actor in actors)
            {
                StatContainer stat = statsContainerList[i];
                actor.updateHealth -= stat.updateHealth;
                actor.updateEnergy -= stat.updateMP;
            }
            selection.cancelTarget -= selectorManager.revertSelection;
        }

        private void invalidSelection()
        {
            menuInputHandler.menuFailSound.Play();
            selection.cancelTarget?.Invoke();
        }


    }

}
