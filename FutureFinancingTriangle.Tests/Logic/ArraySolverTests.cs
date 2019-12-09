using FutureFinancingTriangle.Logic;
using FutureFinancingTriangle.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class ArraySolverTests
    {
        private readonly ISolver _sut;

        public ArraySolverTests()
        {
            _sut = new ArraySolver();
        }

        [Fact]
        public void ReturnsSimpleAnswer_When_OnlyOneNode()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 7 }
                }
            };

            var result = _sut.Solve(inputTriangle);

            Assert.Equal(7, result.PathSum);
            Assert.Equal(1, result.Nodes.Count);
            Assert.Contains(new List<int> { 7 }, result.Nodes.Select(x => x.Value).Contains);
        }
    }
}
