using System.Collections.Generic;
using UnityEngine;

public static class Party
{
    private static List<PartyMember> activeMembers = new List<PartyMember>();
    private static List<PartyMember> reserveMembers = new List<PartyMember>();
    public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;

    static Party()
    {
        Stats soraStats = new Stats(10, 10, 0, 0, 3, 1, 2, 0, 4, 1);
        Stats ashStats = new Stats(10, 10, 0, 0, 3, 1, 2, 0, 1, 1);
        Stats tidusStats = new Stats(10, 10, 0, 0, 3, 1, 2, 0, 5, 1);
        Stats linkStats = new Stats(10, 10, 0, 0, 3, 1, 2, 0, 2, 1);

        PartyMember Sora = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"), soraStats);
        PartyMember Ash = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"), ashStats);
        PartyMember Tidus = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"), tidusStats);
        PartyMember Link = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"), linkStats);

        activeMembers.Add(Sora);
        activeMembers.Add(Tidus);
        activeMembers.Add(Ash);
        activeMembers.Add(Link);
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
