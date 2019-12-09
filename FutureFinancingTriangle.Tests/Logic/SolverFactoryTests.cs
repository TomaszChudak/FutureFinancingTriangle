using FutureFinancingTriangle.Configuration;
using FutureFinancingTriangle.Logic;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class SolverFactoryTests
    {
        private readonly Mock<IOptions<AppSettings>> _configMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private ISolverFactory _sut;

        public SolverFactoryTests()
        {
            _configMock = new Mock<IOptions<AppSettings>>();
            _serviceProviderMock = new Mock<IServiceProvider>();            
        }

        [Fact]
        public void ReturnsGraphSolver_When_GraphSolverInConfig()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings { SolverKind = "Graph" });
            _sut = new SolverFactory(_configMock.Object, _serviceProviderMock.Object);

            var result = _sut.CreateSolver();

            Assert.IsType<GraphSolver>(result);
        }

        [Fact]
        public void ReturnsArraySolver_When_ArraySolverInConfig()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings { SolverKind = "Array" });
            _sut = new SolverFactory(_configMock.Object, _serviceProviderMock.Object);

            var result = _sut.CreateSolver();

            Assert.IsType<ArraySolver>(result);
        }

        [Fact]
        public void ThrowsException_When_UnknownSolverInConfig()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings { SolverKind = "www" });
            _sut = new SolverFactory(_configMock.Object, _serviceProviderMock.Object);

            Action a = () => _sut.CreateSolver();

            Assert.Throws<ApplicationException>(a);
        }
    }
}
