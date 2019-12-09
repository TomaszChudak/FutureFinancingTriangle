using FutureFinancingTriangle.Logic;
using FutureFinancingTriangle.Models;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class GraphCreatorTests
    {
        private readonly IGraphCreator _sut;

        public GraphCreatorTests()
        {
            _sut = new GraphCreator();
        }

        [Fact]
        public void ReturnsRightTopNode()
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

            var result = _sut.Create(inputTriangle);

            Assert.Equal(0, result.Column);
            Assert.Null(result.GoLeftSum);
            Assert.Null(result.GoRightSum);
            Assert.NotNull(result.LeftChild);
            Assert.Equal(0, result.Level);
            Assert.NotNull(result.RightChild);
            Assert.Equal(1, result.Value);

            var node22 = result.RightChild.LeftChild;

            Assert.Equal(1, node22.Column);
            Assert.Null(node22.GoLeftSum);
            Assert.Null(node22.GoRightSum);
            Assert.Null(node22.LeftChild);
            Assert.Equal(2, node22.Level);
            Assert.Null(node22.RightChild);
            Assert.Equal(22, node22.Value);
        }
    }
}
