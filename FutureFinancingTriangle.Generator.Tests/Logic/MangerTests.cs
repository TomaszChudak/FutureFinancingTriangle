using FutureFinancingTriangle.Generator.Input;
using FutureFinancingTriangle.Generator.Logic;
using FutureFinancingTriangle.Generator.Output;
using FutureFinancingTriangle.Generator.Wrappers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FutureFinancingTriangle.Generator.Tests.Output
{
    public class MangerTests
    {
        private Mock<IConsoleReader> _consoleReaderMock;
        private Mock<IConsoleWrapper> _consoleWrapperMock;
        private Mock<IFileWritter> _fileWritterMock;
        private Mock<ITriangleGenerator> _triangleGeneratorMock;
        private IManager _sut;

        public MangerTests()
        {
            _consoleReaderMock = new Mock<IConsoleReader>();
            _consoleWrapperMock = new Mock<IConsoleWrapper>();
            _fileWritterMock = new Mock<IFileWritter>();
            _triangleGeneratorMock = new Mock<ITriangleGenerator>();
            _sut = new Manager(_consoleReaderMock.Object, _consoleWrapperMock.Object, _triangleGeneratorMock.Object, _fileWritterMock.Object);
        }

        [Fact]
        public void DataFromInputArePassedToGeneratorAndSaved()
        {
            _consoleReaderMock.SetupSequence(x => x.ReadPositiveIntFromConsole(It.IsAny<string>()))
                .Returns(10)
                .Returns(99);
            _consoleReaderMock.Setup(x => x.ReadBoolFromConsole(It.IsAny<string>()))
                .Returns(true);
            var fileName = "aaa.txt";
            _consoleReaderMock.Setup(x => x.ReadStringFromConsole(It.IsAny<string>()))
                .Returns(fileName);

            _sut.DoIt();

            _triangleGeneratorMock.Verify(x => x.Generate(10, 99, true), Times.Once);
            _fileWritterMock.Verify(x => x.Save(It.IsAny<IEnumerable<IEnumerable<int>>>(), fileName), Times.Once);
        }
    }
}
