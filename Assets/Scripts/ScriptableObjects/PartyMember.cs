using MGCNTN;

public class PartyMember : MemberBattleInfo
{
    public Stats stats;

    public Stats augmentStats;

    public Equipment equipment = new Equipment();

    public Stats Stats => stats + equipment.getEquipmentTotalStats() + augmentStats!;

}
