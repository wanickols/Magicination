using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Party
    {
        private static List<PartyMember> activeMembers = new List<PartyMember>();
        private static List<PartyMember> reserveMembers = new List<PartyMember>();
        public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;
        public static IReadOnlyList<PartyMember> ReserveMembers => reserveMembers;

        private static int encounterRate = 0;

        private static bool canEncounter = true;

        static Party()
        {
            generateParty();
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

        public static int getEncounterRate()
        {

            //Check Armor for canEncounter Possibly
            if (!canEncounter)
                return 0;

            foreach (PartyMember member in activeMembers)
            {
                encounterRate += member.Stats.ENC;
            }

            return encounterRate;
        }

        private static void generateParty()
        {
            PartyMember Aaron = Resources.Load<PartyMember>(Paths.Aaron);
            PartyMember Kaja = Resources.Load<PartyMember>(Paths.Kaja);
            PartyMember Seth = Resources.Load<PartyMember>(Paths.Seth);
            PartyMember Zera = Resources.Load<PartyMember>(Paths.Zera);

            activeMembers.Add(Aaron);
            activeMembers.Add(Kaja);
            activeMembers.Add(Seth);
            //activeMembers.Add(Zera);

        }
    }
}