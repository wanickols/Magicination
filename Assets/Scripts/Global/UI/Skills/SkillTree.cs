using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGCNTN
{
    public class SkillTree
    {
        private List<TreeNode> nodes;
        private List<List<TreeNode>> nodeGrid = new List<List<TreeNode>>();

        public TreeNode currNode;

        ///Public Functions

        public void reset(GameObject treeParent)
        {
            nodes = treeParent.GetComponentsInChildren<TreeNode>().ToList();
            resetCurrNode();
            initTree();
        }
        public void resetCurrNode()
        {
            if (nodes.Count > 0)
                currNode = nodes[0];
        }



        /// Private Functions
        //Tree Functions
        private void initTree()
        {
            //Creates 8 grid for the 8 allowed columns in UI
            for (int i = 0; i < 8; i++)
                nodeGrid.Add(new List<TreeNode>());

            //Creates columns of all node based on x value
            foreach (TreeNode node in nodes)
                nodeGrid[node.coord.x].Add(node);

            createNeighbors();
        }

        private void createNeighbors()
        {
            for (int col = 0; col < nodeGrid.Count; col++)
            {
                List<TreeNode> columnNodes = nodeGrid[col];

                for (int i = 0; i < columnNodes.Count; i++)
                {
                    TreeNode currentNode = columnNodes[i];

                    // Find the down neighbor if it exists
                    if (i < columnNodes.Count - 1)
                    {
                        TreeNode downNeighbor = columnNodes[i + 1];
                        currentNode.AddNeighbor(downNeighbor, Dir.Down);
                    }

                    // Find the up neighbor if it exists
                    if (i > 0)
                    {
                        TreeNode upNeighbor = columnNodes[i - 1];
                        currentNode.AddNeighbor(upNeighbor, Dir.Up);
                    }

                    // Find the right neighbor if it exists
                    if (col < nodeGrid.Count - 1)
                    {
                        List<TreeNode> nextColumnNodes = nodeGrid[col + 1];
                        TreeNode rightNeighbor = FindClosestRightNeighbor(currentNode, nextColumnNodes);
                        if (rightNeighbor != null)
                        {
                            currentNode.AddNeighbor(rightNeighbor, Dir.Right);
                        }
                    }

                    // Find the left neighbor if it exists
                    if (col > 0)
                    {
                        List<TreeNode> prevColumnNodes = nodeGrid[col - 1];
                        TreeNode leftNeighbor = FindClosestLeftNeighbor(currentNode, prevColumnNodes);
                        if (leftNeighbor != null)
                        {
                            currentNode.AddNeighbor(leftNeighbor, Dir.Left);
                        }
                    }
                }
            }
        }
        private TreeNode FindClosestRightNeighbor(TreeNode currentNode, List<TreeNode> nextColumnNodes)
        {
            TreeNode closestRightNeighbor = null;
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
        private TreeNode FindClosestLeftNeighbor(TreeNode currentNode, List<TreeNode> prevColumnNodes)
        {
            TreeNode closestLeftNeighbor = null;
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