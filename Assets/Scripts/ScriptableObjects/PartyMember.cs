using Battle;
using Core;
using UnityEngine;

public class PartyMember : MemberBattleInfo
{
    [SerializeField] private Stats stats;

    public Equipment equipment = new Equipment();

    public Stats Stats => stats + equipment.getEquipmentTotalStats();
}
