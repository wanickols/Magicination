using System.Collections.Generic;

public static class Party
{
    private static List<PartyMember> activeMembers = new List<PartyMember>();
    private static List<PartyMember> reserveMembers = new List<PartyMember>();
    public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;
    public static IReadOnlyList<PartyMember> ReserveMembers => reserveMembers;

    static Party()
    {
        PartyMember Aaron = ResourceLoader.Load<PartyMember>(ResourceLoader.Aaron);
        PartyMember Kaja = ResourceLoader.Load<PartyMember>(ResourceLoader.Kaja);
        PartyMember Seth = ResourceLoader.Load<PartyMember>(ResourceLoader.Seth);
        PartyMember Zera = ResourceLoader.Load<PartyMember>(ResourceLoader.Zera);

        activeMembers.Add(Aaron);
        activeMembers.Add(Kaja);
        //activeMembers.Add(Seth);
        //activeMembers.Add(Zera);

    }


    public static void AddActiveMember(PartyMember memberToAdd)
    {
        if (activeMembers.Contains(memberToAdd))
            return;

        activeMembers.Add(memberToAdd);
        reserveMembers.Remove(memberToAdd);
    }

    public static void RemoveActiveMember(PartyMember memberToRemove)
    {
        if (!activeMembers.Contains(memberToRemove))
            return;


        activeMembers.Remove(memberToRemove);
        reserveMembers.Add(memberToRemove);
    }
}
