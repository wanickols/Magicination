using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //Mange the turns, trigger the next turn when one is done
    //End battle when it's over

    private List<Actor> TurnOrder = new List<Actor>();
    private int turnNumber = 0;

    private void Awake()
    {
        SpawnPartyMembers();
    }

    private void Update()
    {
        if (TurnOrder[turnNumber].isTakingTurn)
            return;

        CheckForEnd();
        GoToNextTurn();
    }

    private void CheckForEnd()
    {
        //TODO
    }

    private void GoToNextTurn()
    {
        turnNumber = (turnNumber + 1) % TurnOrder.Count;
        TurnOrder[turnNumber].StartTurn();

    }

    private void SpawnPartyMembers()
    {
        Vector2 spawnPostion = new Vector2(-5, -1.8f);
        foreach (PartyMember member in Party.ActiveMembers)
        {
            var temp = Instantiate(member.actorPrefab, spawnPostion, Quaternion.identity);
            spawnPostion.y += 1.2f;
            TurnOrder.Add(temp.GetComponent<Ally>());
        }

    }
}
