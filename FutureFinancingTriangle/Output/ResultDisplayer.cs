using FutureFinancingTriangle.Configuration;
using FutureFinancingTriangle.Models;
using Microsoft.Extensions.Options;
using System;

namespace FutureFinancingTriangle.Output
{
    internal interface IResultDisplayer
    {
        void Display(InputTriangle inputTriangle, ResultPath resutlPath);
    }

    internal class ResultDisplayer : IResultDisplayer
    {
        private readonly int _maxPossibleNumberLength;
        private readonly IConsoleWrapper _consoleWrapper;

        public ResultDisplayer(IOptions<AppSettings> config, IConsoleWrapper consoleWrapper)
        {
            _maxPossibleNumberLength = config.Value.MaximalValue.ToString().Length;
            _consoleWrapper = consoleWrapper;
        }

        public void Display(InputTriangle inputTriangle, ResultPath resultPath)
        {
            for(int i = 0; i < inputTriangle.Levels.Length; i++)
            {
                if (resultPath.PathSum != null)
                {
                    DisplayRegularNodes(inputTriangle.Levels[i].AsSpan(0, resultPath.Nodes[i].Column));
                    DisplayBestNode(inputTriangle.Levels[i][resultPath.Nodes[i].Column]);
                    DisplayRegularNodes(inputTriangle.Levels[i].AsSpan(resultPath.Nodes[i].Column + 1));
                }
                else
                {
                    DisplayRegularNodes(inputTriangle.Levels[i].AsSpan());
                }
                    
                _consoleWrapper.WriteLine();
            }

            if (resultPath.PathSum != null)
            {
                DisplaySussessSummary(resultPath);
            }
            else
            {
                DisplayFailSummary(resultPath);
            }

            _consoleWrapper.WriteLine($"{Environment.NewLine}Press any key to quit");
            _consoleWrapper.ReadKey();
        }

        private void DisplayRegularNodes(Span<int> nodeValues)
        {
            _consoleWrapper.ForegroundColor = ConsoleColor.Gray;

            foreach (int nodeValue in nodeValues)
            {
                DisplayNode(nodeValue);
            }
        }

        private void DisplayBestNode(int nodeValue)
        {
            _consoleWrapper.ForegroundColor = ConsoleColor.Green;
            DisplayNode(nodeValue);
        }

        private void DisplayNode(int nodeValue)
        {
            _consoleWrapper.Write(nodeValue.ToString().PadLeft(_maxPossibleNumberLength + 1, ' '));
        }

        private void DisplaySussessSummary(ResultPath resultPath)
        {
            _consoleWrapper.ResetColor();
            _consoleWrapper.Write($"{Environment.NewLine}After {resultPath.SolvingTime} maximum sum equals ");
            _consoleWrapper.ForegroundColor = ConsoleColor.Blue;
            _consoleWrapper.Write(resultPath.PathSum.ToString());
            _consoleWrapper.ResetColor();
            _consoleWrapper.WriteLine(" has been found");
        }

        private void DisplayFailSummary(ResultPath resultPath)
        {
            _consoleWrapper.ResetColor();
            _consoleWrapper.WriteLine($"{Environment.NewLine}After {resultPath.SolvingTime} no valid path has been found");
        }
    }
}
