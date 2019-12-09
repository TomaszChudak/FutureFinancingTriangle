using FutureFinancingTriangle.Models;
using System;
using System.Collections.Generic;

namespace FutureFinancingTriangle.Logic
{
    internal class ArraySolver : ISolver
    {
        private const int NoAllowedStep = -1;

        public ResultPath Solve(InputTriangle inputTriangle)
        {
            var levelCount = inputTriangle.Levels.Length;
            var leftSums = new int[levelCount][];
            var rightSums = new int[levelCount][];
            var bestSums = new int[levelCount][];

            InitHelpTables(levelCount, leftSums, rightSums, bestSums);

            FillLastLevel(levelCount, leftSums, rightSums, bestSums);

            FillSumArrays(inputTriangle, levelCount, leftSums, rightSums, bestSums);

            return GetResultPath(inputTriangle, leftSums, rightSums, bestSums);            
        }

        private ResultPath GetResultPath(InputTriangle inputTriangle, int[][] leftSums, int[][] rightSums, int[][] bestSums)
        {
            var resultPath = new ResultPath { Nodes = new List<Node>() };

            if (bestSums[0][0] == NoAllowedStep)
                return resultPath;

            resultPath.PathSum = bestSums[0][0] + inputTriangle.Levels[0][0];

            var bestColumn = 0;

            for (int level = 0; level < inputTriangle.Levels.Length; level++)
            {
                resultPath.Nodes.Add(new Node
                {
                    Column = bestColumn,
                    Value = inputTriangle.Levels[level][bestColumn]
                });
                if (level < inputTriangle.Levels.Length - 1
                    && bestSums[level][bestColumn] == rightSums[level][bestColumn])
                    bestColumn++;
            }

            return resultPath;
        }

        private void InitHelpTables(int levelCount, int[][] leftSums, int[][] rightSums, int[][] bestSums)
        {
            for (int level = 0; level < levelCount; level++)
            {
                leftSums[level] = new int[level + 1];
                rightSums[level] = new int[level + 1];
                bestSums[level] = new int[level + 1];
            }
        }

        private void FillLastLevel(int levelCount, int[][] leftSums, int[][] rightSums, int[][] bestSums)
        {
            for (int column = 0; column < levelCount; column++)
            {
                leftSums[levelCount - 1][column] = 0;
                rightSums[levelCount - 1][column] = 0;
                bestSums[levelCount - 1][column] = 0;
            }
        }

        private void FillSumArrays(InputTriangle inputTriangle, int levelCount, int[][] leftSums, int[][] rightSums, int[][] bestSums)        {

            for (int level = levelCount - 2; level >= 0; level--)
            {
                var nextLevel = level + 1;
                for (int column = 0; column <= level; column++)
                {
                    var nextColumn = column + 1;
                    if (bestSums[nextLevel][column] != NoAllowedStep
                        && (inputTriangle.Levels[level][column] % 2) + (inputTriangle.Levels[nextLevel][column] % 2) == 1)
                    {
                        leftSums[level][column] = inputTriangle.Levels[nextLevel][column] + bestSums[nextLevel][column];
                    }
                    else
                    {
                        leftSums[level][column] = NoAllowedStep;
                    }
                    if (bestSums[nextLevel][nextColumn] != NoAllowedStep
                        && (inputTriangle.Levels[level][column] % 2) + (inputTriangle.Levels[nextLevel][nextColumn] % 2) == 1)
                    {
                        rightSums[level][column] = inputTriangle.Levels[nextLevel][nextColumn] + bestSums[nextLevel][nextColumn];
                    }
                    else
                    {
                        rightSums[level][column] = NoAllowedStep;
                    }
                    bestSums[level][column] = Math.Max(leftSums[level][column], rightSums[level][column]);
                }
            }
        }
    }
}
