using FutureFinancingTriangle.Models;
using System.Collections.Generic;

namespace FutureFinancingTriangle.Logic
{
    internal interface IGraphCreator
    {
        Node Create(InputTriangle inputTriangle);
    }

    class GraphCreator : IGraphCreator
    {
        public Node Create(InputTriangle inputTriangle)
        {
            var lowerGraphLevel = new List<Node>();

            for (int level = inputTriangle.Levels.Length - 1; level >= 0; level--)
            {
                var graphLevel = new List<Node>();

                for(int column = 0; column < inputTriangle.Levels[level].Length; column++)
                {
                    var node = new Node { Value = inputTriangle.Levels[level][column], Level = level, Column = column};

                    if(level < inputTriangle.Levels.Length - 1)
                    {
                        node.LeftChild = lowerGraphLevel[column];
                        node.RightChild = lowerGraphLevel[column + 1];
                    }

                    graphLevel.Add(node);
                }

                lowerGraphLevel = graphLevel;
            }

            return lowerGraphLevel[0];
        }
    }
}
