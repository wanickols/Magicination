using MGCNTN;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Member Battle Info", menuName = "Member Battle Info")]
public class MemberBattleInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject actorPrefab;

    [SerializeField] private Sprite menuPortrait;


    ///Stats
    public virtual Stats baseStats { get; set; }

    [HideInInspector] public List<AugmentStats> augmentingStats = new List<AugmentStats>();

    public Stats Stats { get; private set; }

    public void updateStats()
    {
        Stats combinedStats = new Stats();

        // Add individual stats
        if (baseStats != null)
            combinedStats += baseStats;

        int damageTaken = 0, energyUsed = 0;

        if (Stats != null)
        {
            damageTaken = Stats.MAXHP - Stats.HP;
            energyUsed = Stats.MAXENG - Stats.ENG;
        }
        // Add augmenting stats
        foreach (var augmentingStat in augmentingStats)
        {
            combinedStats += augmentingStat;
        }

        Stats = combinedStats;

        Stats.HP -= damageTaken;
        Stats.ENG -= energyUsed;
    }


    ///Status
    public StatusCollection Statuses = new StatusCollection();


    public PlayableCharacters playableCharacter;
    public string DisplayName => _name;
    public GameObject ActorPrefab => actorPrefab;

    public Sprite MenuPortrait => menuPortrait;
}
