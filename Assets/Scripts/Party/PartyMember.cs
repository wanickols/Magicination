using UnityEngine;

public class PartyMember
{
    public GameObject actorPrefab { get; private set; }
    public Stats Stats { get; private set; }

    public PartyMember(GameObject actorPrefab, Stats stats)
    {
        this.actorPrefab = actorPrefab;
        this.Stats = stats;
    }
}
