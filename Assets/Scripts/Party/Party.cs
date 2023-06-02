using System.Collections.Generic;
using UnityEngine;

public static class Party
{
    private static List<PartyMember> activeMembers = new List<PartyMember>();
    private static List<PartyMember> reserveMembers = new List<PartyMember>();
    public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;

    static Party()
    {
        PartyMember Sora = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"));
        PartyMember Ash = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"));
        PartyMember Tidus = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"));
        PartyMember Link = new PartyMember(Resources.Load<GameObject>("BattlePrefabs/PartyMember"));

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
