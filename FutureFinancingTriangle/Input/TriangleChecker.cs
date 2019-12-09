using FutureFinancingTriangle.Configuration;
using FutureFinancingTriangle.Models;
using Microsoft.Extensions.Options;
using System.Linq;

namespace FutureFinancingTriangle.Input
{
    internal interface ITriangleChecker
    {
        bool Check(InputTriangle inputTriangle, out string error);
    }

    internal class TriangleChecker : ITriangleChecker
    {
        private readonly int _maxValue;

        public TriangleChecker(IOptions<AppSettings> config)
        {
            _maxValue = config.Value.MaximalValue;
        }

        public bool Check(InputTriangle inputTriangle, out string error)
        {
            for(int i = 0; i < inputTriangle.Levels.Length; i++)
            {
                if(!CheckNumberOfNodesInLine(inputTriangle.Levels[i], i))
                {
                    error = $"Wrong number of nodes in line {i + 1}";
                    return false;
                }
                if (!CheckMaximalValue(inputTriangle.Levels[i]))
                {
                    error = $"At least value of node in line {i + 1} is above maximum alloved value: {_maxValue}";
                    return false;
                }
            }

            error = null;
            return true;
        }

        private bool CheckNumberOfNodesInLine(int[] levelNodes, int levelIndex)
        {
            return levelNodes.Length == levelIndex + 1;
        }

        private bool CheckMaximalValue(int[] levelNodes)
        {
            return levelNodes.All(x => x <= _maxValue);            
        }
    }
}
