using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Battle : MonoBehaviour
    {


        //Mange the turns, trigger the next turn when one is done
        //End battle when it's over

        private EnemyGenerator enemyGenerator;
        private TurnBar turnBar;
        private EnemyPack enemyPack;
        public static Region currentRegion;


        private PriorityQueue<Actor, float> turnQueue = new PriorityQueue<Actor, float>(); // Use a priority queue to store the actors and their turn times
        private List<Actor> nextSix = new List<Actor>();
        private List<Ally> allies = new List<Ally>();
        private List<Enemy> enemies = new List<Enemy>();
        private int turnNumber = 0;
        private bool setUpComplete = false;
        private PlayerBattlePositions battlePositions = new PlayerBattlePositions();

        private Actor currentActor;

        private const float baseTime = 25f;
        private const float randomFactor = .2f;

        public IReadOnlyList<Ally> Allies => allies;
        public IReadOnlyList<Enemy> Enemies => enemies;

        private void Awake()
        {
            turnBar = FindObjectOfType<TurnBar>();
            enemyGenerator = new EnemyGenerator(currentRegion);
            SpawnPartyMembers();
            SpawnEnemies();

        }



        private void Update()
        {
            if (!setUpComplete)
            {
                DetermineTurnOrder();
            };

            if (currentActor.isTakingTurn) return;

            CheckForEnd();
            GoToNextTurn();
        }

        private void CheckForEnd()
        {
            //TODO
        }

        private void GoToNextTurn()
        {
            currentActor = nextSix[0];
            nextSix.RemoveAt(0);
            nextSix.Add(turnQueue.Dequeue());
            turnBar.SpawnPortraitSlots(nextSix);
            currentActor.StartTurn();
            currentActor.turnTime += CalculateTurnTime(currentActor.baseTurnSpeed);
            print(currentActor.name + "Time: " + currentActor.turnTime);
            turnQueue.Enqueue(currentActor, currentActor.turnTime);
        }

        private void SpawnPartyMembers()
        {
            SpawnCounts partyCount = (SpawnCounts)Core.Party.ActiveMembers.Count;

            List<Vector2> positionList = battlePositions.getPositions(partyCount);

            int i = 0;
            foreach (PartyMember member in Core.Party.ActiveMembers)
            {
                var temp = Instantiate(member.ActorPrefab, positionList[i], Quaternion.identity);
                Ally ally = temp.GetComponent<Ally>();
                ally.Stats = member.Stats;

                turnQueue.Enqueue(ally, CalculateTurnTime(ally.baseTurnSpeed));
                allies.Add(ally);
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
                enemy.Stats = enemyPack.Enemies[i].Stats;
                turnQueue.Enqueue(enemy, CalculateTurnTime(enemy.baseTurnSpeed));
                enemies.Add(enemy);

            }

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
            turnBar.SpawnPortraitSlots(nextSix);
        }

        private float CalculateTurnTime(float speed)
        {
            Debug.Log("Speed: " + speed);
            // Use the formula to calculate the turn time based on speed and other factors
            return baseTime * speed + UnityEngine.Random.Range(-randomFactor, randomFactor);
        }
    }
}
