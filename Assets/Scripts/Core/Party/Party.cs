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

        public static Arsenal arsenal = new Arsenal();
        public static ItemBag bag = new ItemBag();

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
            int encounterRate = 0;
            //Check Armor for canEncounter Possibly
            if (canEncounter)
            {
                foreach (PartyMember member in activeMembers)
                {
                    encounterRate += member.Stats.ENC;
                }
            }

            return encounterRate;
        }

        private static void generateParty()
        {
            PartyMember Zane = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Zane));
            PartyMember Leon = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Leon));
            PartyMember Seth = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Seth));
            PartyMember Aurora = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Aurora));

            activeMembers.Add(Zane);
            activeMembers.Add(Leon);
            //activeMembers.Add(Seth);
            activeMembers.Add(Aurora);

            Equippable rustySword = Resources.Load<Equippable>("items/equipment/weapons/RustySword");
            Equippable testChest = Resources.Load<Equippable>("items/equipment/armor/TestChest");
            Equippable jadeRing = Resources.Load<Equippable>("items/equipment/accessories/Jade Ring");

            arsenal.Add(rustySword);
            arsenal.Add(testChest);
            arsenal.Add(jadeRing);

            Consumable potion = Resources.Load<Consumable>("items/consumables/potion");
            Consumable revive = Resources.Load<Consumable>("items/consumables/revive");

            bag.Add(potion);
            bag.Add(revive);
        }
    }
}