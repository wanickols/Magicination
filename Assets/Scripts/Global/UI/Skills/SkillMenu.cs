using UnityEngine;

namespace MGCNTN
{

    public class SkillMenu : MonoBehaviour
    {
        ///Private Parameters
        [SerializeField] private GameObject treeParent; //curently one, need to make more
        [SerializeField] private TreeSelector treeSelector;
        [SerializeField] private GameObject ActionBar;

        //Components
        private SkillTree tree = new SkillTree();
        private ActionBar actionBar;

        public PartyMember member;
        public Skill skill => tree.currNode.transform.GetComponent<SkillHolder>().skill;

        ///Unity Functions
        private void Awake()
        {

            tree.reset(treeParent);
            treeSelector.tree = tree;

            actionBar = GetComponentInChildren<ActionBar>();
            ActionBar.SetActive(false);
        }

        ///Public Functions
        public void openActionBar()
        {

            int mp = member.Stats.MP;

            ActionBar.SetActive(true);

            if (!skill.Data.menuUsable || mp < skill.Data.cost)
                actionBar.deactivateOption(0);
        }

        public void closeActionBar() => ActionBar.SetActive(false);

        ///Private Functions
    }
}
