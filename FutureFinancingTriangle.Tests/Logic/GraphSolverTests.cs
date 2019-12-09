using FutureFinancingTriangle.Logic;
using FutureFinancingTriangle.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class GraphSolverTests
    {
        private readonly Mock<IGraphCreator> _graphCreatorMock;
        private readonly ISolver _sut;

        public GraphSolverTests()
        {
            _graphCreatorMock = new Mock<IGraphCreator>();
            _sut = new GraphSolver(_graphCreatorMock.Object);
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

            _graphCreatorMock.Setup(x => x.Create(inputTriangle))
                .Returns(new Node { Value = 7 });

            var result = _sut.Solve(inputTriangle);

            Assert.Equal(7, result.PathSum);
            Assert.Equal(1, result.Nodes.Count);
            Assert.Contains(new List<int> { 7 }, result.Nodes.Select(x => x.Value).Contains);
        }
    }
}
