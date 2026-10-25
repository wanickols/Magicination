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
    protected virtual Stats baseStats { get; set; }

    [HideInInspector] public List<AugmentStats> augmentingStats = new List<AugmentStats>();
    [HideInInspector] public List<Stats> additionalStats = new List<Stats>();

    protected Stats addOnStats { get; set; }
    public Stats Stats => baseStats + addOnStats;

    public void incrementDamage(int ammount) { baseStats.DamageTaken += ammount; }
    public void incrementEnergyUsed(int ammount) { baseStats.EnergyUsed += ammount; }

    public virtual void updateStats()
    {
        addOnStats = new Stats();

        // Add augmenting stats
        foreach (var augmentingStat in augmentingStats)
        {
            addOnStats += augmentingStat;
        }

        //Add Permenent Stats
        foreach(var additionalStat in additionalStats)
        {
            addOnStats += additionalStat;
        }
    }


    ///Status
    public StatusCollection Statuses = new StatusCollection();


    ///Skills
    public SkillsCollection activeSkills = new SkillsCollection();

    public PlayableCharacters playableCharacter;
    public string DisplayName => _name;
    public GameObject ActorPrefab => actorPrefab;

    public Sprite MenuPortrait => menuPortrait;
}
