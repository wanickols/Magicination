using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGCNTN
{
    public class TreeSelector : Selector
    {

        ///Private 
        [SerializeField] private GameObject treeParent, skillWindow;
        private List<SkillHolder> skillHolders;

        ///Public
        public SkillTree tree;


        ///Unity Functions
        protected override void Awake()
        {
            base.Awake();
            type = SelectorType.Tree;
            skillHolders = treeParent.GetComponentsInChildren<SkillHolder>().ToList();
            tree.resetCurrNode();
        }

        protected override void Update()
        {
            if (rectTransform.anchoredPosition != tree.currNode.rectTransform.anchoredPosition)
                MoveToSelectedOption();

            if (scrollable)
                UpdateScrollPosition();
        }

        ///Public Functions
        public SkillNode currNode => tree.currNode;

        public void setNode(SkillNode node) => tree.currNode = node;


        public SkillHolder find(SkillNode node)
        {
            SkillHolder holder = null;

            foreach (SkillHolder h in skillHolders)
            {
                if (h.isNode(node))
                    holder = h;
            }

            return holder;
        }


        ///Protected Functions
        //Selector Override
        protected override void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, tree.currNode.rectTransform.anchoredPosition, selectorSpeed);
        }




    }
}