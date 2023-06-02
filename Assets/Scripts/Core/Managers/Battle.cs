using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //Mange the turns, trigger the next turn when one is done
    //End battle when it's over

    [SerializeField] private List<Actor> TurnOrder = new List<Actor>();
    [SerializeField] private int turnNumber = 0;

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
}
