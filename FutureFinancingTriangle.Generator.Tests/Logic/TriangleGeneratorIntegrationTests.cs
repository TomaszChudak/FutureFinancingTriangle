using System.Linq;
using FutureFinancingTriangle.Generator.Logic;
using Xunit;

namespace FutureFinancingTriangle.Generator.Tests
{
    public class TriangleGeneratorIntegrationTests
    {
        private readonly IRandomser _randomiser;
        private readonly ITriangleGenerator _sut;

        public TriangleGeneratorIntegrationTests()
        {
            _randomiser = new Randomiser();
            _sut = new TriangleGenerator(_randomiser);
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(10, 9)]
        [InlineData(100, 9)]
        [InlineData(100, 99)]
        [InlineData(50, 777)]
        public void GenerateRandomTriangle_When_NoSubsequentOddEven(int numberOfLevels, int maxNumber)
        {
            var result = _sut.Generate(numberOfLevels, maxNumber, false);

            Assert.Equal(numberOfLevels, result.Count());

            var allNumbers = result.SelectMany(x => x);
            Assert.Contains(allNumbers, x => x <= maxNumber);
            Assert.Equal(GetNumbersCount(numberOfLevels), allNumbers.Count());
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(10, 9)]
        [InlineData(100, 9)]
        [InlineData(100, 99)]
        [InlineData(50, 777)]
        public void GenerateSubsequentOddEventTriangle_When_SubsequentOddEven(int numberOfLevels, int maxNumber)
        {
            var result = _sut.Generate(numberOfLevels, maxNumber, true);

            Assert.Equal(numberOfLevels, result.Count());

            var allNumbers = result.SelectMany(x => x);
            Assert.Contains(allNumbers, x => x <= maxNumber);
            Assert.Equal(GetNumbersCount(numberOfLevels), allNumbers.Count());

            var firstLineOdd = result.First().First() % 2 == 1;
            for(int i = 0; i < numberOfLevels; i++)
            {
                var expectedOdd = i % 2 == 0 ? firstLineOdd : !firstLineOdd;
                Assert.All<int>(result.Skip(i).First(), (x) => Assert.True(x % 2 == (expectedOdd ? 1 : 0)));
            }
        }

        private int GetNumbersCount(int numberOfLevels)
        {
            if (numberOfLevels == 1)
                return 1;
            return numberOfLevels * numberOfLevels - GetNumbersCount(numberOfLevels - 1);
        }
    }
}
