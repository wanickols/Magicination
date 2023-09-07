
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{

    public class SkillMenu : MonoBehaviour
    {
        ///Public Parameters

        public Skill skill => tree.currNode.transform.GetComponent<SkillHolder>().skill;

        [NonSerialized] public PartyMember member;

        ///Private Parameters
        [SerializeField] private GameObject treesParent; //curently one, need to make more
        [SerializeField] private TreeSelector treeSelector;
        [SerializeField] private GameObject ActionBar;

        //Components
        private SkillTree tree = new SkillTree();
        private ActionBar actionBar;
        private List<GameObject> treeParents = new List<GameObject>();

        private int currTree = 0;


        ///Unity Functions
        private void Awake()
        {
            foreach (Transform t in treesParent.GetComponentInChildren<Transform>())
            {
                if (t.GetComponent<Selector>())
                    continue;

                t.gameObject.SetActive(false);
                treeParents.Add(t.gameObject);

            }

            treeSelector.tree = tree;

            tree.reset(treeParents[currTree]);
            treeParents[currTree].SetActive(true);

            actionBar = GetComponentInChildren<ActionBar>();
            ActionBar.SetActive(false);
        }

        ///Public Functions
        public void selectTree(int i)
        {
            treeParents[currTree].SetActive(false);
            currTree = i;
            tree.reset(treeParents[i]);
            treeParents[currTree].SetActive(true);
        }
        public void openActionBar()
        {

            int mp = member.Stats.MP;

            ActionBar.SetActive(true);

            if (!skill.Data.menuUsable || mp < skill.Data.cost)
                actionBar.deactivateOption(1);

        }

        public void closeActionBar() => ActionBar.SetActive(false);

        ///Private Functions
    }
}
