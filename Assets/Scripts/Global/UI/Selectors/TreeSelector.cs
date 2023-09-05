using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGCNTN
{
    public class TreeSelector : Selector
    {

        [SerializeField] private GameObject treeParent;
        List<SkillNode> nodes;

        List<List<SkillNode>> nodeGrid = new List<List<SkillNode>>();

        public SkillNode currNode;

        ///Unity Functions
        private void OnEnable()
        {
            if (nodes.Count > 0)
                currNode = nodes[0];
        }

        protected override void Awake()
        {
            base.Awake();
            type = SelectorType.Tree;

            nodes = treeParent.GetComponentsInChildren<SkillNode>().ToList();


            initTree();
        }

        protected override void Update()
        {
            if (rectTransform.anchoredPosition != currNode.rectTransform.anchoredPosition)
                MoveToSelectedOption();

            if (scrollable)
                UpdateScrollPosition();
        }

        //Selector Override
        protected override void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, currNode.rectTransform.anchoredPosition, selectorSpeed);
        }

        /// Private Functions
        //Tree Functions
        private void initTree()
        {
            //Creates 8 grid for the 8 allowed columns in UI
            for (int i = 0; i < 8; i++)
                nodeGrid.Add(new List<SkillNode>());

            //Creates columns of all node based on x value
            foreach (SkillNode node in nodes)
                nodeGrid[node.coord.x].Add(node);

            createNeighbors();
        }

        private void createNeighbors()
        {
            for (int col = 0; col < nodeGrid.Count; col++)
            {
                List<SkillNode> columnNodes = nodeGrid[col];

                for (int i = 0; i < columnNodes.Count; i++)
                {
                    SkillNode currentNode = columnNodes[i];

                    // Find the down neighbor if it exists
                    if (i < columnNodes.Count - 1)
                    {
                        SkillNode downNeighbor = columnNodes[i + 1];
                        currentNode.AddNeighbor(downNeighbor, Dir.Down);
                    }

                    // Find the up neighbor if it exists
                    if (i > 0)
                    {
                        SkillNode upNeighbor = columnNodes[i - 1];
                        currentNode.AddNeighbor(upNeighbor, Dir.Up);
                    }

                    // Find the right neighbor if it exists
                    if (col < nodeGrid.Count - 1)
                    {
                        List<SkillNode> nextColumnNodes = nodeGrid[col + 1];
                        SkillNode rightNeighbor = FindClosestRightNeighbor(currentNode, nextColumnNodes);
                        if (rightNeighbor != null)
                        {
                            currentNode.AddNeighbor(rightNeighbor, Dir.Right);
                        }
                    }

                    // Find the left neighbor if it exists
                    if (col > 0)
                    {
                        List<SkillNode> prevColumnNodes = nodeGrid[col - 1];
                        SkillNode leftNeighbor = FindClosestLeftNeighbor(currentNode, prevColumnNodes);
                        if (leftNeighbor != null)
                        {
                            currentNode.AddNeighbor(leftNeighbor, Dir.Left);
                        }
                    }
                }
            }
        }
        private SkillNode FindClosestRightNeighbor(SkillNode currentNode, List<SkillNode> nextColumnNodes)
        {
            SkillNode closestRightNeighbor = null;
            int closestMagnitude = int.MaxValue;

            foreach (var node in nextColumnNodes)
            {
                int distanceX = node.coord.x - currentNode.coord.x;
                int distanceY = node.coord.y - currentNode.coord.y;
                int magnitude = Mathf.Abs(distanceX) + Mathf.Abs(distanceY);

                // Check if the node is to the right of the current node
                if (distanceX > 0 && magnitude < closestMagnitude)
                {
                    closestMagnitude = magnitude;
                    closestRightNeighbor = node;
                }
            }

            return closestRightNeighbor;
        }
        private SkillNode FindClosestLeftNeighbor(SkillNode currentNode, List<SkillNode> prevColumnNodes)
        {
            SkillNode closestLeftNeighbor = null;
            int closestMagnitude = int.MaxValue;

            foreach (var node in prevColumnNodes)
            {
                int distanceX = currentNode.coord.x - node.coord.x;
                int distanceY = node.coord.y - currentNode.coord.y;
                int magnitude = Mathf.Abs(distanceX) + Mathf.Abs(distanceY);

                // Check if the node is to the left of the current node
                if (distanceX > 0 && magnitude < closestMagnitude)
                {
                    closestMagnitude = magnitude;
                    closestLeftNeighbor = node;
                }
            }

            return closestLeftNeighbor;
        }


    }
}