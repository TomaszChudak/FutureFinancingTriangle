using FutureFinancingTriangle.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FutureFinancingTriangle.Logic
{
    internal class GraphSolver : ISolver
    {
        private readonly IGraphCreator _graphCreator;

        internal GraphSolver(IGraphCreator graphCreator)
        {
            _graphCreator = graphCreator;
        }

        public ResultPath Solve(InputTriangle inputTriangle)
        {
            var topNode = _graphCreator.Create(inputTriangle);

            VisitNode(topNode, inputTriangle.Levels.Length);

            var resultPath = new ResultPath { Nodes = new List<Node>()};

            if (topNode.GoLeftSum != null || topNode.GoRightSum != null)
            {
                FillResultPath(resultPath, topNode);
            }

            return resultPath;
        }

        private void VisitNode(Node node, int levelsCount)
        {
            if (IsChildNodeAllowed(node, node.LeftChild))
            {
                VisitNode(node.LeftChild, levelsCount);
                node.GoLeftSum = GetMaxFromTwo(node.LeftChild.GoLeftSum, node.LeftChild.GoRightSum) + node.LeftChild.Value;
            }
            if (IsChildNodeAllowed(node, node.RightChild))
            {
                VisitNode(node.RightChild, levelsCount);
                node.GoRightSum = GetMaxFromTwo(node.RightChild.GoLeftSum, node.RightChild.GoRightSum) + node.RightChild.Value;
            }
            if(node.Level == levelsCount - 1)
            {
                node.GoLeftSum = 0;
                node.GoRightSum = 0;
            }
        }

        private int? GetMaxFromTwo(int? first, int? second)
        {
            if (first == null)
                return second;
            if(second == null)
                return first;
            return Math.Max(first.Value, second.Value);
        }

        private bool IsChildNodeAllowed(Node parentNode, Node childNode)
        {
            return childNode != null && (parentNode.Value + childNode.Value) % 2 == 1;
        }

        private void FillResultPath(ResultPath resultPath, Node bestNode)
        {
            if (!resultPath.Nodes.Any())
                resultPath.PathSum = GetMaxFromTwo(bestNode.GoLeftSum, bestNode.GoRightSum) + bestNode.Value;

            if (bestNode == null)
                return;

            resultPath.Nodes.Add(bestNode);

            if(bestNode.GoLeftSum != null && bestNode.GoRightSum == null
                || bestNode.GoLeftSum >= bestNode.GoRightSum)
            {
                FillResultPath(resultPath, bestNode.LeftChild);
            }
            else if(bestNode.GoRightSum != null && bestNode.GoLeftSum == null
                || bestNode.GoRightSum >= bestNode.GoLeftSum)
            {
                FillResultPath(resultPath, bestNode.RightChild);
            }
        }
    }
}
