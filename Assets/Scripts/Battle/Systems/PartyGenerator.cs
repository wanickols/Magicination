using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class PartyGenerator
    {

        private PlayerBattlePositions battlePositions = new PlayerBattlePositions();

        public List<Actor> Spawn(TurnSystem turnSystem, BattleUIManager battleUI)
        {
            SpawnCounts partyCount = (SpawnCounts)Party.ActiveMembers.Count;

            List<Vector2> positionList = battlePositions.getPositions(partyCount);

            List<Actor> allies = new List<Actor>();

            int i = 0;
            foreach (PartyMember member in Party.ActiveMembers)
            {
                var temp = GameObject.Instantiate(member.ActorPrefab, positionList[i], Quaternion.identity);
                Actor ally = temp.GetComponent<Actor>();
                ally.setMemberBattleInfo(member);
                turnSystem.enqueue(ally);
                allies.Add(ally);
                ally.checkDeath(true); //Kills
                battleUI.AddPartyMemberUI(member);
                battleUI.LinkListeners(ally);
                i++;
            }

            return allies;
        }
    }
}
