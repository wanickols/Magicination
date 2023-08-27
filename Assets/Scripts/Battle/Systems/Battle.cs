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

        /// Public parameters
        //Data from Core
        public static Region currentRegion;
        public static BattleStates battleState = BattleStates.battle;

        public bool inputAllowed = false;
        /// Private parameters
        //Basic

        private bool isEnding = false;

        // Other Battle Handler/Managers/Classes
        private EnemyGenerator enemyGenerator;
        private PartyGenerator partyGenerator;

        [Header("Managers")]
        [SerializeField] private BattleUIManager battleUI = new BattleUIManager();
        [SerializeField] private TurnBar turnBar;
        [SerializeField] private BattleData data;

        private TurnSystem turnSystem;


        /// Public functions

        //Listeners
        public void tryRun() { endBattle?.Invoke(); }
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
            battleState = BattleStates.battle;
            battleUI.init(data, this);
            enemyGenerator = new EnemyGenerator(currentRegion);
            partyGenerator = new PartyGenerator();
            turnSystem = new TurnSystem(battleUI, data);


        }
        private void Start()
        {
            //List Creation
            data.allies = partyGenerator.Spawn(turnSystem, battleUI);
            data.setEnemyData(enemyGenerator.generate(turnSystem));

            InitActions();
            turnSystem.DetermineTurnOrder(turnBar);
        }
        private void Update()
        {
            if (battleState != BattleStates.battle)
            {
                inputAllowed = false;
                return;
            }



            if (turnSystem.isTakingTurn)
            {
                if (data.currentActor.type == ActorType.Ally && inputAllowed)
                    battleUI.update();

                return;
            }
            inputAllowed = true;


            if (isEnding)
                return;

            turnSystem.NextTurn(turnBar);
        }


        /// Private Functions

        //Actions
        private void InitActions()
        {
            battleUI.selection.selectTarget += selectTarget;
            battleUI.run += tryRun;

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
            //Select Target
            List<Actor> targets = battleUI.selection.targets;

            switch (battleUI.currBattleSelection)
            {
                case BattleMainSelections.Attack:
                    data.currentActor.commander.attack(targets);
                    break;
                case BattleMainSelections.Items:
                    battleUI.revertToMain();
                    Consumable item = battleUI.getItem();
                    data.currentActor.commander.useItem(targets, item);
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
            battleUI.selection.selectTarget -= selectTarget;
            battleUI.run -= tryRun;
            turnSystem.nextTurn -= battleUI.toggleBattleMenu;

            foreach (Actor enemy in data.enemies)
                data.updateAllies -= enemy.ai.updateTargets;

            foreach (Actor ally in data.allies)
            {
                if (ally.ai)
                    data.updateEnemies -= ally.ai.updateTargets;
            }

            data.clearData();
        }
    }
}
