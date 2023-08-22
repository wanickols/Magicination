using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PartyTargetMenu : MonoBehaviour
    {

        public PartyTargetSelections selectionType { get; private set; }

        /// Private Parametres
        [SerializeField] private GameObject partyMemberTargetPrefab;
        [SerializeField] private GameObject selectorPrefab;
        [SerializeField] private GameObject content;

        private List<PartyMemberTarget> targets = new List<PartyMemberTarget>();
        private Consumable currItem;

        /// Public Functions
        public Selector initTargets(PartyTargetSelections selectionType, Consumable item)
        {
            this.selectionType = selectionType;

            if (item != null)
                currItem = item;

            foreach (PartyMember partyMember in Party.ActiveMembers)
            {
                PartyMemberTarget target = Instantiate(partyMemberTargetPrefab, content.transform).GetComponent<PartyMemberTarget>();
                target.setMember(partyMember);
                targets.Add(target);
            }

            foreach (PartyMember partyMember in Party.ReserveMembers)
            {
                PartyMemberTarget target = Instantiate(partyMemberTargetPrefab, content.transform).GetComponent<PartyMemberTarget>();
                target.setMember(partyMember);
                targets.Add(target);
            }

            Selector selector = Instantiate(selectorPrefab, content.transform).GetComponent<Selector>();
            selector.type = SelectorType.ScrollerVertical;
            return selector;
        }

        public void Select(int selected)
        {
            currItem.Consume();
        }

        public void addTarget(PartyMember member)
        {

            PartyMemberTarget target = Instantiate(partyMemberTargetPrefab, content.transform).GetComponent<PartyMemberTarget>();
            target.setMember(member);
            targets.Add(target);
        }

        public void refreshTargets()
        {
            if (targets.Count > 0)
            {
                int i = 0;
                foreach (PartyMember partyMember in Party.ActiveMembers)
                {
                    targets[i].setMember(partyMember);
                    i++;
                }

                foreach (PartyMember partyMember in Party.ReserveMembers)
                {
                    targets[i].setMember(partyMember);
                    i++;
                }
            }
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
