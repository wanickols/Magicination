using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class Battle : MonoBehaviour
    {
        /// Actions
        public static Action endBattle;
        public static Action quit;
        public static Action<List<Actor>> Attack;
        public static Action<List<Actor>, IConsumable> UseItem;


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

        [Header("Managers")]
        [SerializeField] private BattleUIManager battleUI = new BattleUIManager();
        [SerializeField] private TurnBar turnBar;
        [SerializeField] private BattleData data;

        private TurnSystem turnSystem;

        /// Public functions

        //Listeners
        public void tryRun() { endBattle?.Invoke(); }

        public void trySelect(BattleMainSelections type)
        {
            if (battleState == BattleStates.battle)
            {
                battleState = BattleStates.select;
                StartCoroutine(selection.CO_SelectSingleTarget(data.allies, data.enemies, type));
                battleUI.setBattleMenu(false);
            }
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

        public void death()
        {
            bool alliesDead = true;
            data.setTargets();
            foreach (Actor ally in data.allies)
            {
                if (ally.IsDead) continue;
                else
                {
                    alliesDead = false;
                    break;
                }
            }

            if (alliesDead)
            {
                tryEnd(false);
                return;
            }

            bool enemiesDead = true;
            foreach (Actor enemy in data.enemies)
            {
                if (enemy.IsDead) continue;
                else
                {
                    enemiesDead = false;
                    break;
                }
            }

            if (enemiesDead)
            {
                tryEnd(true);
                return;
            }

            turnSystem.DetermineTurnOrder(turnBar);
        }

        /// Unity Functions
        private void Awake()
        {
            battleUI.init(data, this);
            selection = new Selection();
            enemyGenerator = new EnemyGenerator(currentRegion);
            partyGenerator = new PartyGenerator();
            turnSystem = new TurnSystem(battleUI, data);


        }
        private void Start()
        {
            //List Creation
            data.allies = partyGenerator.Spawn(turnSystem, battleUI);
            data.setliveAllies();
            data.setEnemyData(enemyGenerator.generate(turnSystem));

            InitActions();
            turnSystem.DetermineTurnOrder(turnBar);
        }
        private void Update()
        {
            if (battleState != BattleStates.battle)
                return;

            battleUI.update();

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

            List<Actor> targets = selection.targets;

            switch (selection.currSelectionType)
            {
                case BattleMainSelections.Attack:
                    Attack?.Invoke(targets);
                    break;
                case BattleMainSelections.Items:
                    Consumable item = battleUI.getItem();
                    UseItem?.Invoke(targets, item);
                    break;
                default:
                    Debug.Log("Battle Selection Type Not Implemented in Battle Manager");
                    break;
            }
            battleState = BattleStates.battle;
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
