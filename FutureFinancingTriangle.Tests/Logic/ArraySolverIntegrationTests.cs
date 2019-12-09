using FutureFinancingTriangle.Logic;
using FutureFinancingTriangle.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class ArraySolverIntegrationTests
    {
        private readonly ISolver _sut;

        public ArraySolverIntegrationTests()
        {
            _sut = new ArraySolver();
        }

        [Fact]
        public void ReturnsSimpleAnswer_When_SimpleTriangle()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 1 },
                    new int[] { 8, 9 },
                    new int[] { 1, 5, 9 },
                    new int[] { 4, 5, 2, 3 }
                }
            };

            var result = _sut.Solve(inputTriangle);

            Assert.Equal(16, result.PathSum);
            Assert.Equal(4, result.Nodes.Count);
            Assert.Contains(new List<int> { 1, 8, 5, 2 }, result.Nodes.Select(x => x.Value).Contains);
        }
    }
}
