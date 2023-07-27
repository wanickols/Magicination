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
        public static Action<List<Actor>> Attack;

        /// Public parameters
        //Data from Core
        public static Region currentRegion;


        /// Private parameters
        // Other Battle Handler/Managers/Classes
        private EnemyGenerator enemyGenerator;
        private PartyGenerator partyGenerator;
        [SerializeField] private BattleUIManager battleUI = new BattleUIManager();
        private Selection selection;

        private BattleStates battleState = BattleStates.battle;
        private EventSystem eventSystem;
        [SerializeField] private TurnBar turnBar;

        [SerializeField] private BattleData data;


        private TurnSystem turnSystem;

        /// Public functions
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


        /// Private functions
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
            }

            foreach (Actor ally in data.allies)
            {
                if (ally.ai)
                    data.updateEnemies += ally.ai.updateTargets;
            }

            data.setTargets();
        }

        private void Update()
        {
            if (battleState == BattleStates.select)
                return;

            if (turnSystem.isTakingTurn)
                return;

            CheckForEnd();
            turnSystem.NextTurn(turnBar);
        }


        private void selectTarget()
        {
            if (!selection.hasTarget)
                return;

            battleState = BattleStates.battle;


            List<Actor> targets = selection.targets;
            eventSystem.enabled = true;

            Battle.Attack?.Invoke(targets);
        }

        // Turns 
        private void CheckForEnd()
        {
            //TODO
        }


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
