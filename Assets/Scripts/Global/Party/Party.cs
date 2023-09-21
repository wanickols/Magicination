using MGCNTN.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class Party : Savable
    {
        private static List<PartyMember> activeMembers = new List<PartyMember>();
        private static List<PartyMember> reserveMembers = new List<PartyMember>();
        public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;
        public static IReadOnlyList<PartyMember> ReserveMembers => reserveMembers;

        protected override string customPath { get => "Playtime/Partymembers.json"; }

        public static Arsenal arsenal = new Arsenal();
        public static ItemBag bag = new ItemBag();

        private static bool canEncounter = true;

        protected override void Awake()
        {
            generateParty();
            //SaveData();
            //LoadData();
        }
        public void AddActiveMember(PartyMember memberToAdd)
        {
            if (activeMembers.Contains(memberToAdd))
                return;

            activeMembers.Add(memberToAdd);
            reserveMembers.Remove(memberToAdd);
        }

        public void RemoveActiveMember(PartyMember memberToRemove)
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

        private void generateParty()
        {
            activeMembers.Clear();


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

        }

        public override bool SaveData()
        {
            List<string> jsons = new List<string>();

            foreach (PartyMember member in activeMembers)
                jsons.Add(member.SaveToFile());

            foreach (PartyMember member in reserveMembers)
                jsons.Add(member.SaveToFile());

            if (jsons.Count <= 0)
                return false;

            saveToFile(jsons);

            return true;
        }

        public override bool LoadData()
        {
            string[] jsons = loadFromFile();

            if (jsons == null)
                return false;

            activeMembers.Clear();
            int i = 0;

            while (i < 4 && activeMembers.Count < jsons.Length)
            {
                foreach (string json in jsons)
                    activeMembers.Add(JsonToPartyMember(json));
            }

            if (jsons.Length > 4)
                for (int j = i; j < jsons.Length; i++)
                    reserveMembers.Add(JsonToPartyMember(jsons[j]));


            return true;
        }

        private PartyMember JsonToPartyMember(string json)
        {
            PartyMember member = new PartyMember();
            member.LoadFromFile(json);

            return member;
        }
    }
}