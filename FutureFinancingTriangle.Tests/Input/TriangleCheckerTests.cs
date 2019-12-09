using FutureFinancingTriangle.Configuration;
using FutureFinancingTriangle.Input;
using FutureFinancingTriangle.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class TriangleCheckerTests
    {
        private readonly Mock<IOptions<AppSettings>> _configMock;
        private readonly TriangleChecker _sut;

        public TriangleCheckerTests()
        {
            _configMock = new Mock<IOptions<AppSettings>>();
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings { MaximalValue = 99 });
            _sut = new TriangleChecker(_configMock.Object);
        }

        [Fact]
        public void True_When_RightInputTriangle()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 1 },
                    new int[] { 4, 6 },
                    new int[] { 11, 22, 66 }
                }
            };

            var result = _sut.Check(inputTriangle, out string error);

            Assert.True(result);
            Assert.Null(error);
        }

        [Fact]
        public void False_When_WrongNumberNodesInLine()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 1 },
                    new int[] { 4, 6, 8 },
                    new int[] { 11, 22, 66 }
                }
            };

            var result = _sut.Check(inputTriangle, out string error);

            Assert.False(result);
            Assert.Contains("Wrong number of nodes in line", error);
        }

        [Fact]
        public void False_When_MaximumAllowedValueExceeded()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 1 },
                    new int[] { 4, 6 },
                    new int[] { 11, 112, 66 }
                }
            };

            var result = _sut.Check(inputTriangle, out string error);

            Assert.False(result);
            Assert.Contains("is above maximum alloved value", error);
        }
    }
}
