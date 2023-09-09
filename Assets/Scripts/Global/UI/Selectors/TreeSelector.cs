using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class TreeSelector : Selector
    {

        ///Public Parameters
        public SkillTree tree;
        [NonSerialized] public GameObject treeParent;

        ///Private Parameters
        private List<SkillHolder> skillHolders;

        ///Unity Functions
        protected override void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            animator = GetComponent<Animator>();
            type = SelectorType.Tree;
        }

        protected override void Update()
        {
            if (rectTransform.anchoredPosition != tree.currNode.rectTransform.anchoredPosition)
                MoveToSelectedOption();

            if (scrollable)
                UpdateScrollPosition();
        }

        ///Public Functions
        public TreeNode currNode => tree.currNode;

        public void setNode(TreeNode node) => tree.currNode = node;


        public SkillHolder find(TreeNode node)
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