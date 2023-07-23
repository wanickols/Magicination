using Battle;
using UnityEngine;

public class PartyMember : BattleData
{
    [SerializeField] private Stats stats;

    public Stats Stats => stats;
}
