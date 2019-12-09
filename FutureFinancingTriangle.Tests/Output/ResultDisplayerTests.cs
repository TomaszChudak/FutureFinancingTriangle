using FutureFinancingTriangle.Configuration;
using FutureFinancingTriangle.Models;
using FutureFinancingTriangle.Output;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class ResultDisplayerTests
    { 
        private readonly Mock<IOptions<AppSettings>> _configMock;
        private readonly Mock<IConsoleWrapper> _consoleWrapperMock;
        private IResultDisplayer _sut;

        public ResultDisplayerTests()
        {
            _configMock = new Mock<IOptions<AppSettings>>();
            _consoleWrapperMock = new Mock<IConsoleWrapper>();
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings { MaximalValue = 99 });
            _sut = new ResultDisplayer(_configMock.Object, _consoleWrapperMock.Object);
        }

        [Fact]
        public void DisplaysOneRow_when_SimplestTriangleGiven()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 7 }
                }
            };

            var resultPath = new ResultPath { Nodes = new List<Node> { new Node { Value = 7 } }, PathSum = 7, SolvingTime = TimeSpan.FromSeconds(2) };
            _sut.Display(inputTriangle, resultPath);

            _consoleWrapperMock.Verify(x => x.Write("  7"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(y => y.Contains("maximum"))), Times.Once);
            _consoleWrapperMock.Verify(x => x.WriteLine(It.Is<string>(y => y.Contains("quit"))), Times.Once);
        }

        [Fact]
        public void DisplaysTenRow_when_TenRowTriangleGiven()
        {
            var inputTriangle = new InputTriangle
            {
                Levels = new int[][]
                {
                    new int[] { 1 },
                    new int[] { 2, 3 },
                    new int[] { 4, 5, 6 }
                }
            };

            var resultPath = new ResultPath { Nodes = new List<Node> { new Node { Value = 7 }, new Node { Value = 2 }, new Node { Value = 5 } }, PathSum = 14, SolvingTime = TimeSpan.FromSeconds(3) };
            _sut.Display(inputTriangle, resultPath);

            _consoleWrapperMock.Verify(x => x.Write("  1"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write("  2"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write("  3"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write("  4"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write("  5"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write("  6"), Times.Once);
            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(y => y.Contains("maximum"))), Times.Once);
            _consoleWrapperMock.Verify(x => x.WriteLine(It.Is<string>(y => y.Contains("quit"))), Times.Once);
        }
    }
}
