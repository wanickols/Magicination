using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class PartyTargetMenu : MonoBehaviour
    {
        public PartyTargetSelections selectionType { get; private set; }
        /// Private Parametres

        [SerializeField] private GameObject selectorPrefab;
        [SerializeField] private GameObject content;

        private List<PartyMemberTarget> targets = new List<PartyMemberTarget>();
        private ObjectData data;
        private Stats user;

        /// Public Functions
        public void initTargets(PartyTargetSelections selectionType, ObjectData data, Stats user)
        {
            this.selectionType = selectionType;
            this.data = data;
            this.user = user;

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
            data.use(targets[selected].member, user);


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
