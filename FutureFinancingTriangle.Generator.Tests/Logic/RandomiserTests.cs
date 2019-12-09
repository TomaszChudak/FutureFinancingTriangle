using FutureFinancingTriangle.Generator.Logic;
using System.Collections.Generic;
using Xunit;
using static FutureFinancingTriangle.Generator.Logic.Randomiser;

namespace FutureFinancingTriangle.Generator.Tests
{
    public class RandomiserTests
    {
        private readonly IRandomser _sut;

        public RandomiserTests()
        {
            _sut = new Randomiser();
        }

        [Fact]
        public void RandomNextBoolReturnsExpectedValues()
        {
            var watchdog = 10000;
            var wasTrue = false;
            var wasFalse = false;

            while (wasFalse == false && wasTrue == false && watchdog > 0)
            {
                var result = _sut.RandomNextBool();

                if (result)
                    wasTrue = true;
                else
                    wasFalse = true;

                watchdog--;
            }

            Assert.True(watchdog > 0);
        }

        [Theory]
        [InlineData(true, 100)]
        [InlineData(false, 200)]
        [InlineData(null, 200)]
        [InlineData(true, 5)]
        [InlineData(false, 60)]
        [InlineData(null, 10)]
        public void RandomNextIntReturnsExpectedValues(bool? oddEvenBoth, int maxNumber)
        {
            var count = 100;            
            var oddEven = oddEvenBoth == null ? OddEven.Any : (oddEvenBoth.Value ? OddEven.Odd: OddEven.Even);
            var resultList = new List<int>();

            while (count > 0)
            {
                var result = _sut.RandomNextInt(maxNumber, oddEven);

                count--;
            }

            Assert.All(resultList, (x) => Assert.True(x <= maxNumber));

            if (oddEven == OddEven.Even)
                Assert.All(resultList, (x) => Assert.True(x % 2 == 0));
            else if (oddEven == OddEven.Odd)
                Assert.All(resultList, (x) => Assert.True(x % 2 == 1));
        }
    }
}
