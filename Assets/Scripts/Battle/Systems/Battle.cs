using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle
{
    public class Battle : MonoBehaviour
    {
        /// Actions
        public static Action endBattle;
        public static Action quit;
        public static Action<List<Actor>> Attack;

        /// Public parameters
        //Data from Core
        public static Region currentRegion;


        /// Private parameters

        //Basic
        private BattleStates battleState = BattleStates.battle;
        private bool isEnding = false;

        // Other Battle Handler/Managers/Classes
        private EnemyGenerator enemyGenerator;
        private PartyGenerator partyGenerator;
        private Selection selection;
        private EventSystem eventSystem;

        [Header("Managers")]
        [SerializeField] private BattleUIManager battleUI = new BattleUIManager();
        [SerializeField] private TurnBar turnBar;
        [SerializeField] private BattleData data;

        private TurnSystem turnSystem;

        /// Public functions

        //Listeners
        public void tryRun()
        {
            endBattle?.Invoke();
        }

        public void tryAttack()
        {
            if (battleState == BattleStates.battle)
            {
                eventSystem.enabled = false;
                battleState = BattleStates.select;
                StartCoroutine(selection.CO_SelectSingleTarget(data.allies, data.enemies));
                battleUI.setBattleMenu(false);
            }
        }

        public void death()
        {
            bool alliesDead = true;
            foreach (Actor ally in data.allies)
            {
                if (ally.isDead) continue;
                else
                {
                    alliesDead = false;
                    break;
                }
            }

            if (alliesDead)
            {
                tryEnd(false);
            }


            bool enemiesDead = true;
            foreach (Actor enemy in data.enemies)
            {
                if (enemy.isDead) continue;
                else
                {
                    enemiesDead = false;
                    break;
                }
            }

            if (enemiesDead)
            {
                tryEnd(true);
            }

            turnSystem.DetermineTurnOrder(turnBar);

        }
        private void tryEnd(bool win)
        {


            battleUI.hideUI();
            isEnding = true;

            if (!win)
            {
                StartCoroutine(battleUI.CO_GameOver());
            }
            else
            {
                //Show Victory Stuff
                endBattle?.Invoke();
            }
        }

        /// Unity Functions
        private void Awake()
        {
            eventSystem = FindAnyObjectByType<EventSystem>();

            battleUI.initData(data);
            selection = new Selection();
            enemyGenerator = new EnemyGenerator(currentRegion);
            partyGenerator = new PartyGenerator();
            turnSystem = new TurnSystem(battleUI, data);

            //List Creation
            data.allies = partyGenerator.Spawn(turnSystem, battleUI);
            data.setEnemyData(enemyGenerator.generate(turnSystem));
        }
        private void Start()
        {
            InitActions();
            turnSystem.DetermineTurnOrder(turnBar);
        }
        private void Update()
        {
            if (battleState == BattleStates.select)
                return;

            if (turnSystem.isTakingTurn)
                return;

            if (isEnding)
                return;

            turnSystem.NextTurn(turnBar);
        }


        /// Private Functions

        //Actions
        private void InitActions()
        {
            selection.selectTarget += selectTarget;

            turnSystem.nextTurn += battleUI.toggleBattleMenu;
            InitTargets();
        }
        private void InitTargets()
        {
            foreach (Actor enemy in data.enemies)
            {
                data.updateAllies += enemy.ai.updateTargets;
                enemy.Death += death;
            }

            foreach (Actor ally in data.allies)
            {
                ally.Death += death;
                if (ally.ai)
                    data.updateEnemies += ally.ai.updateTargets;
            }

            data.setTargets();
        }

        //BattleState
        private void selectTarget()
        {
            if (!selection.hasTarget)
                return;

            battleState = BattleStates.battle;


            List<Actor> targets = selection.targets;
            eventSystem.enabled = true;

            Battle.Attack?.Invoke(targets);
        }

        //Deconstructor
        private void OnDestroy()
        {
            selection.selectTarget -= selectTarget;
            turnSystem.nextTurn -= battleUI.toggleBattleMenu;

            foreach (Actor enemy in data.enemies)
            {
                data.updateAllies -= enemy.ai.updateTargets;
            }

            foreach (Actor ally in data.allies)
            {
                if (ally.ai)
                    data.updateEnemies -= ally.ai.updateTargets;
            }

            data.clearData();
        }
    }
}
