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

    public Stats Stats
    {
        get
        {
            // Create a new Stats object to hold the combined stats
            Stats combinedStats = new Stats();

            // Add individual stats
            if (baseStats != null)
                combinedStats += baseStats;

            // Add augmenting stats
            foreach (var augmentingStat in augmentingStats)
            {
                combinedStats += augmentingStat;
            }

            return combinedStats;
        }
    }


    ///Status
    public StatusCollection Statuses { get; set; } //


    public PlayableCharacters playableCharacter;
    public string DisplayName => _name;
    public GameObject ActorPrefab => actorPrefab;

    public Sprite MenuPortrait => menuPortrait;
}
