using FutureFinancingTriangle.Input;
using FutureFinancingTriangle.Logic;
using FutureFinancingTriangle.Models;
using FutureFinancingTriangle.Output;
using Moq;
using System;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class DispatcherTests
    {
        private readonly Mock<IErrorDisplayer> _errorDisplayerMock;
        private readonly Mock<IResultDisplayer> _resultDisplayerMock;
        private readonly Mock<ISolverFactory> _solverFactoryMock;
        private readonly Mock<ITriangleReader> _triangleReaderMock;
        private readonly Mock<ITriangleChecker> _triangleCheckerMock;
        private readonly IDispatcher _sut;

        public DispatcherTests()
        {
            _errorDisplayerMock = new Mock<IErrorDisplayer>();
            _resultDisplayerMock = new Mock<IResultDisplayer>();
            _solverFactoryMock = new Mock<ISolverFactory>();
            _triangleCheckerMock = new Mock<ITriangleChecker>();
            _triangleReaderMock = new Mock<ITriangleReader>();
            _sut = new Dispatcher(_errorDisplayerMock.Object, _resultDisplayerMock.Object, _solverFactoryMock.Object, _triangleCheckerMock.Object, _triangleReaderMock.Object);
        }

        [Fact]
        public void DisplayError_When_ReadingThrownException()
        {
            _triangleReaderMock.Setup(x => x.Read())
                .Throws<FormatException>();

            _sut.Proceed();

            _errorDisplayerMock.Verify(x => x.Display(It.IsAny<string>()), Times.Once);
            _resultDisplayerMock.Verify(x => x.Display(It.IsAny<InputTriangle>(), It.IsAny<ResultPath>()), Times.Never);
        }

        [Fact]        
        public void DisplayError_When_InputTriangleHasErrors()            
        {
            var error = "aaa";
            _triangleCheckerMock.Setup(x => x.Check(It.IsAny<InputTriangle>(), out error))
                .Returns(false);

            _sut.Proceed();

            _errorDisplayerMock.Verify(x => x.Display(error), Times.Once);
            _resultDisplayerMock.Verify(x => x.Display(It.IsAny<InputTriangle>(), It.IsAny<ResultPath>()), Times.Never);
        }

        [Fact]
        public void DisplayError_When_SolverThrownException()
        {
            var error = (string)null;
            _triangleCheckerMock.Setup(x => x.Check(It.IsAny<InputTriangle>(), out error))
                .Returns(true);
            _solverFactoryMock.Setup(x => x.CreateSolver())
                .Returns((GraphSolver)null);

            _sut.Proceed();

            _errorDisplayerMock.Verify(x => x.Display(It.IsAny<string>()), Times.Once);
            _resultDisplayerMock.Verify(x => x.Display(It.IsAny<InputTriangle>(), It.IsAny<ResultPath>()), Times.Never);
        }

        [Fact]
        public void DisplayResult_When_EverythingIsRight()
        {
            var error = (string)null;
            _triangleCheckerMock.Setup(x => x.Check(It.IsAny<InputTriangle>(), out error))
                .Returns(true);
            var solverMock = new Mock<ISolver>();
            solverMock.Setup(x => x.Solve(It.IsAny<InputTriangle>()))
                .Returns(new ResultPath());
            _solverFactoryMock.Setup(x => x.CreateSolver())
                .Returns(solverMock.Object);
            
            _sut.Proceed();

            _errorDisplayerMock.Verify(x => x.Display(It.IsAny<string>()), Times.Never);
            _resultDisplayerMock.Verify(x => x.Display(It.IsAny<InputTriangle>(), It.IsAny<ResultPath>()), Times.Once);
        }
    }
}
