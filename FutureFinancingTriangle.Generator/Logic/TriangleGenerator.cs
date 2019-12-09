using System.Collections.Generic;

namespace FutureFinancingTriangle.Generator.Logic
{
    internal interface ITriangleGenerator
    {
        IEnumerable<IEnumerable<int>> Generate(int numberOfLevels, int maxNumber, bool subsequentOddEven);
    }

    internal class TriangleGenerator : ITriangleGenerator
    {
        private IRandomser _randomiser;

        public TriangleGenerator(IRandomser randomser)
        {
            _randomiser = randomser;
        }

        public IEnumerable<IEnumerable<int>> Generate(int numberOfLevels, int maxNumber, bool subsequentOddEven)
        {
            var result = new List<List<int>>();

            var oddEven = subsequentOddEven
                ? (_randomiser.RandomNextBool() ? Randomiser.OddEven.Even : Randomiser.OddEven.Odd)
                : Randomiser.OddEven.Any;

            for (int i = 0; i < numberOfLevels; i++)
            {
                var row = new List<int>();

                for (int j = 0; j <= i; j++)
                {
                    var value = _randomiser.RandomNextInt(maxNumber, oddEven);

                    row.Add(value);
                }

                result.Add(row);
                oddEven = _randomiser.SwitchOddEven(oddEven);
            }

            return result;
        }
    }
}
