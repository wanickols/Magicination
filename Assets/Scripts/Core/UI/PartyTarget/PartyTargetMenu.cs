using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PartyTargetMenu : MonoBehaviour
    {

        public PartyTargetSelections selectionType { get; private set; }

        /// Private Parametres

        [SerializeField] private GameObject selectorPrefab;
        [SerializeField] private GameObject content;

        private List<PartyMemberTarget> targets = new List<PartyMemberTarget>();
        private Consumable currItem;

        /// Public Functions
        public void initTargets(PartyTargetSelections selectionType, Consumable item)
        {
            this.selectionType = selectionType;

            if (item != null)
                currItem = item;

            int activeCount = Party.ActiveMembers.Count;

            int i = 0;
            foreach (Transform t in content.transform)
            {
                PartyMemberTarget target = t.GetComponent<PartyMemberTarget>();
                if (i < activeCount)
                {
                    t.gameObject.SetActive(true);
                    target.setMember(Party.ActiveMembers[i]);
                    targets.Add(target);
                }
                else if (i < (activeCount + Party.ReserveMembers.Count))
                {
                    t.gameObject.SetActive(true);
                    target.setMember(Party.ActiveMembers[i - activeCount]);
                    targets.Add(target);
                }
                else
                    t.gameObject.SetActive(false);

                i++;
            }
        }
        public void Select(int selected)
        {
            currItem.Consume(targets[selected].member.stats);
            targets[selected].updateValues();
        }

        public void updateData()
        {
            foreach (PartyMemberTarget target in targets)
            {
                target.updateValues();
            }
        }
    }
}
