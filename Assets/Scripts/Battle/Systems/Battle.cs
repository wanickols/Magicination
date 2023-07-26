using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle
{
    public class Battle : MonoBehaviour
    {
        private enum BattleStates
        {
            battle,
            select
        };



        /// Actions
        public static Action endBattle;
        public static Action<List<Actor>> Attack;

        /// Private
        // Other Battle Handler/Managers/Classes
        private EnemyGenerator enemyGenerator;
        private BattleUIManager battleUI;
        private PlayerBattlePositions battlePositions = new PlayerBattlePositions();
        private BattleStates battleState = BattleStates.battle;
        private EventSystem eventSystem;

        //List of Actors
        private Actor currentActor;
        private PriorityQueue<Actor, float> turnQueue = new PriorityQueue<Actor, float>(); // Use a priority queue to store the actors and their turn times
        private List<Actor> nextSix = new List<Actor>();
        private List<Ally> allies = new List<Ally>();
        private List<Enemy> enemies = new List<Enemy>();
        private EnemyPack enemyPack;

        //Flags
        private bool setUpComplete = false;


        //Turn Stuff
        private const float baseTime = 25f;
        private const float randomFactor = .2f;

        /// Public
          //Data from Core
        public static Region currentRegion;

        //Functions
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
                StartCoroutine(battleUI.CO_SelctSingleTarget(allies, enemies));
                battleUI.setBattleMenu(false);
            }
        }

        /// Private
        private void Awake()
        {
            battleUI = this.transform.GetComponent<BattleUIManager>();
            enemyGenerator = new EnemyGenerator(currentRegion);
            eventSystem = FindAnyObjectByType<EventSystem>();
            SpawnPartyMembers();
            SpawnEnemies();
        }


        private void Update()
        {
            if (battleState == BattleStates.select)
            {
                CheckSelect();
                return;
            }

            if (!setUpComplete)
            {
                DetermineTurnOrder();
            };

            if (currentActor.isTakingTurn) return;

            CheckForEnd();
            GoToNextTurn();
        }
        private void CheckSelect()
        {
            if (!battleUI.hasTarget)
                return;

            battleState = BattleStates.battle;


            List<Actor> targets = battleUI.targets;
            eventSystem.enabled = true;

            Battle.Attack?.Invoke(targets);
        }

        // Turns 
        private void CheckForEnd()
        {
            //TODO
        }

        private void GoToNextTurn()
        {
            currentActor = nextSix[0];
            nextSix.RemoveAt(0);
            nextSix.Add(turnQueue.Dequeue());
            battleUI.turnBar.SpawnPortraitSlots(nextSix);
            currentActor.StartTurn();
            currentActor.turnTime += CalculateTurnTime(currentActor.baseTurnSpeed);
            print(currentActor.name + "Time: " + currentActor.turnTime);
            turnQueue.Enqueue(currentActor, currentActor.turnTime);

            if (currentActor.GetComponent<BattlerAI>())
            {
                battleUI.setBattleMenu(false);
            }
            else
                battleUI.setBattleMenu(true);
        }

        private void DetermineTurnOrder()
        {
            // TODO: Fix when less than 6 battle members are on screen

            Actor nextActor = turnQueue.Dequeue();
            currentActor = nextActor;

            // Create a loop to fill the list with the next six actors from the queue
            for (int i = 0; i < 6; i++)
            {
                // Add them to the list
                nextSix.Add(nextActor);

                // Increase their turn time by the base time


                nextActor.turnTime += CalculateTurnTime(nextActor.baseTurnSpeed);
                print(nextActor.name + "Time: " + nextActor.turnTime);
                // Add them back to the queue with their updated turn time
                turnQueue.Enqueue(nextActor, nextActor.turnTime);

                // Get the next actor in the queue
                nextActor = turnQueue.Dequeue();
            }

            // Set up complete flag to true
            setUpComplete = true;

            // Spawn the portrait slots for the next six actors in the list
            battleUI.turnBar.SpawnPortraitSlots(nextSix);
        }

        private float CalculateTurnTime(float speed)
        {
            Debug.Log("Speed: " + speed);
            // Use the formula to calculate the turn time based on speed and other factors
            return baseTime * speed + UnityEngine.Random.Range(-randomFactor, randomFactor);
        }

        // Spawning
        private void SpawnPartyMembers()
        {
            SpawnCounts partyCount = (SpawnCounts)Core.Party.ActiveMembers.Count;

            List<Vector2> positionList = battlePositions.getPositions(partyCount);

            int i = 0;
            foreach (PartyMember member in Core.Party.ActiveMembers)
            {
                var temp = Instantiate(member.ActorPrefab, positionList[i], Quaternion.identity);
                Ally ally = temp.GetComponent<Ally>();
                ally.setBattleData(member.Stats, member.MenuPortrait);
                turnQueue.Enqueue(ally, CalculateTurnTime(ally.baseTurnSpeed));
                allies.Add(ally);

                battleUI.AddPartyMemberUI(member);
                battleUI.LinkListeners(ally);

                i++;
            }

        }

        private void SpawnEnemies()
        {
            enemyPack = enemyGenerator.generateEnemies();

            for (int i = 0; i < enemyPack.Enemies.Count; i++)
            {
                Vector2 spawnPos = new Vector2(enemyPack.SpawnCoordinates[i].x, enemyPack.SpawnCoordinates[i].y);
                GameObject enemyActor = Instantiate(enemyPack.Enemies[i].ActorPrefab, spawnPos, Quaternion.identity);
                Enemy enemy = enemyActor.GetComponent<Enemy>();
                enemy.setBattleData(enemyPack.Enemies[i].Stats, enemyPack.Enemies[i].MenuPortrait);
                turnQueue.Enqueue(enemy, CalculateTurnTime(enemy.baseTurnSpeed));
                enemies.Add(enemy);

            }

        }
    }
}
