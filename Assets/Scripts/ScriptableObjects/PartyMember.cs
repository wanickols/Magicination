using MGCNTN;

public class PartyMember : MemberBattleInfo
{
    public Stats stats;

    public Equipment equipment = new Equipment();

    public Stats Stats => stats + equipment.getEquipmentTotalStats();
}
