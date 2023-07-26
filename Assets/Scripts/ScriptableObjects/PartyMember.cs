using Battle;
using UnityEngine;

public class PartyMember : MemberBattleInfo
{
    [SerializeField] private Stats stats;

    public Stats Stats => stats;
}
