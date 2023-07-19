using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class TurnBar : MonoBehaviour
    {
        [SerializeField] private GameObject portraitSlotPrefab;
        private List<TurnPortrait> slots = new List<TurnPortrait>();


        private void Awake()
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject slot = Instantiate(portraitSlotPrefab, this.gameObject.transform);
                TurnPortrait turn = slot.GetComponentInChildren<TurnPortrait>();
                slots.Add(turn);
            }


        }

        public void SpawnPortraitSlots(List<Actor> actors)
        {
            int i = 0;
            foreach (Actor actor in actors)
            {
                slots[i].setSprite(actor.battlePortrait);
                i++;
            }
        }

        //Cycle Turns

    }
}