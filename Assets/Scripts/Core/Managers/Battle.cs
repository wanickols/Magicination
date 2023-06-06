using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //Mange the turns, trigger the next turn when one is done
    //End battle when it's over
    public static EnemyPack enemyPack;

    private List<Actor> turnOrder = new List<Actor>();
    private int turnNumber = 0;
    private bool setUpComplete = false;

    public IReadOnlyList<Actor> TurnOrder => turnOrder;

    private void Awake()
    {
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
        Vector2 spawnPostion = new Vector2(-5, -1.8f);
        foreach (PartyMember member in Party.ActiveMembers)
        {
            var temp = Instantiate(member.actorPrefab, spawnPostion, Quaternion.identity);
            Ally ally = temp.GetComponent<Ally>();
            ally.Stats = member.Stats;
            turnOrder.Add(ally);

            spawnPostion.y += 1.2f;

        }

    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyPack.Enemies.Count; i++)
        {
            Vector2 spawnPos = new Vector2(enemyPack.XSpawnCoordinates[i], enemyPack.YSpawnCoordinates[i]);
            GameObject enemyActor = Instantiate(enemyPack.Enemies[i].ActorPrefab, spawnPos, Quaternion.identity);
            Enemy enemy = enemyActor.GetComponent<Enemy>();
            enemy.Stats = enemyPack.Enemies[i].Stats;
            turnOrder.Add(enemyActor.GetComponent<Enemy>());

        }
    }

    private void DetermineTurnOrder()
    {
        turnOrder = turnOrder.OrderByDescending(actor => actor.Stats.Initative).ToList();
        foreach (var actor in turnOrder)
        {
            Debug.Log($"{actor.name} {actor.Stats.EVS}");
        }
        turnOrder[0].StartTurn();
        setUpComplete = true;
    }


    private void Start()
    {

    }
}
