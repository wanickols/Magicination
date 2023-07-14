using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class Battle : MonoBehaviour
    {


        //Mange the turns, trigger the next turn when one is done
        //End battle when it's over

        private EnemyGenerator enemyGenerator;
        private EnemyPack enemyPack;
        public static Region currentRegion;

        private List<Actor> turnOrder = new List<Actor>();
        private List<Ally> allies = new List<Ally>();
        private List<Enemy> enemies = new List<Enemy>();
        private int turnNumber = 0;
        private bool setUpComplete = false;
        private PlayerBattlePositions battlePositions = new PlayerBattlePositions();

        public IReadOnlyList<Actor> TurnOrder => turnOrder;
        public IReadOnlyList<Ally> Allies => allies;
        public IReadOnlyList<Enemy> Enemies => enemies;

        private void Awake()
        {
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

            if (turnOrder[turnNumber].isTakingTurn) return;

            CheckForEnd();
            GoToNextTurn();
        }

        private void CheckForEnd()
        {
            //TODO
        }

        private void GoToNextTurn()
        {
            turnNumber = (turnNumber + 1) % turnOrder.Count;
            turnOrder[turnNumber].StartTurn();

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
                turnOrder.Add(ally);
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
                turnOrder.Add(enemyActor.GetComponent<Enemy>());
                enemies.Add(enemy);

            }

        }


        private void DetermineTurnOrder()
        {
            turnOrder = turnOrder.OrderByDescending(actor => actor.Stats.Initative).ToList();
            /*foreach (var actor in turnOrder)
            {
                Debug.Log($"{actor.name} {actor.Stats.EVS}");
            }*/
            turnOrder[0].StartTurn();
            setUpComplete = true;
        }


        private void Start()
        {

        }
    }
}
