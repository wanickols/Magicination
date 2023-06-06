using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    //Store actor's stats and methods for taking a turn
    protected Vector2 startingPosition;
    [SerializeField] protected Vector2 targetPosition;
    public Stats Stats { get; set; }

    public bool isTakingTurn { get; protected set; } = false;
    protected virtual void Start()
    {
        startingPosition = transform.position;

    }
    public abstract void StartTurn();

    protected virtual IEnumerator Co_MoveToAttack()
    {
        float elapsedTime = 0;

        while ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.Lerp(startingPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    protected virtual IEnumerator Co_MoveToStarting()
    {
        float elapsedTime = 0;

        while ((Vector2)transform.position != startingPosition)
        {
            transform.position = Vector2.Lerp(transform.position, startingPosition, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isTakingTurn = false;
    }

}
