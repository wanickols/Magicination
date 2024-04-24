using MGCNTN.Core;
using System.Collections.Generic;
using System.IO;
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

        protected override string errorMessage { get => "Error in party Saving and Loading"; }

        public Arsenal arsenal = new Arsenal();
        public ItemBag bag = new ItemBag();

        private static bool canEncounter = true;

        public Party() : base()
        {
            //generateParty();
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


            PartyMember Zane = GameObject.Instantiate(Resources.Load<PartyMember>(Paths.getCharacterPath(PlayableCharacters.Zane)));
            PartyMember Leon = GameObject.Instantiate(Resources.Load<PartyMember>(Paths.getCharacterPath(PlayableCharacters.Leon)));
            PartyMember Seth = GameObject.Instantiate(Resources.Load<PartyMember>(Paths.getCharacterPath(PlayableCharacters.Seth)));
            PartyMember Aurora = GameObject.Instantiate(Resources.Load<PartyMember>(Paths.getCharacterPath(PlayableCharacters.Aurora)));

            activeMembers.Add(Zane);
            activeMembers.Add(Leon);
            //activeMembers.Add(Seth);
            activeMembers.Add(Aurora);



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

            //test();

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
            string[] jsons = File.ReadAllLines(json);

            PlayableCharacters playableCharacter = JsonUtility.FromJson<PlayableCharacters>(jsons[0]);


            PartyMember member = GameObject.Instantiate(Resources.Load<PartyMember>(Paths.getCharacterPath(playableCharacter)));
            member.equipment.arsenal = arsenal;
            member.updateStats();

            Debug.Assert(member != null, "Error: Part member creation was given invalid playable character in save file");

            member.LoadFromFile(json);

            return member;
        }


        private void test()
        {
            //DEbug
            Consumable potion = Resources.Load<Consumable>("items/consumables/potion");
            //Consumable revive = Resources.Load<Consumable>("items/consumables/revive");

            //bag.Add(potion);

        }
    }
}