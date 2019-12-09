using FutureFinancingTriangle.Input;
using System;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class LineParserTests
    {
        private readonly ILineParser _sut;

        public LineParserTests()
        {
            _sut = new LineParser();
        }

        [Theory]
        [InlineData("1", new int[] { 1 })]
        [InlineData("10 20", new int[] { 10, 20 })]
        [InlineData("111 333 4", new int[] { 111, 333, 4 })]
        public void ReturnsExpectedOutput_When_RightInput(
            string inputLine,
            int[] expectedOutput)
        {
            var result = _sut.Parse(inputLine);

            Assert.Equal(expectedOutput, result);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("10, 20")]
        [InlineData("111 - 333 - 4")]
        public void ThrowsException_When_WrongInput(
            string inputLine)
        {
            Action a = () => _sut.Parse(inputLine);
            Assert.Throws<FormatException>(a);
        }
    }
}
