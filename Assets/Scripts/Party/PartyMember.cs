using UnityEngine;
using UnityEngine.UI;

public class PartyMember
{
    public string Name { get; private set; }
    public GameObject actorPrefab { get; private set; }
    public Stats Stats { get; private set; }
    public Image portrait { get; private set; }

    public PartyMember(string name, Image portrait, GameObject actorPrefab, Stats stats)
    {
        Name = name;
        this.portrait = portrait;
        this.actorPrefab = actorPrefab;
        this.Stats = stats;
    }
}
