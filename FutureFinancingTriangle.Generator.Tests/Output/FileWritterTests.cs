using FutureFinancingTriangle.Generator.Output;
using FutureFinancingTriangle.Generator.Wrappers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FutureFinancingTriangle.Generator.Tests.Output
{
    public class FileWritterTests
    {
        private Mock<IConsoleWrapper> _consoleWrapperMock;
        private Mock<IFileWrapper> _fileWrapperMock;        
        private IFileWritter _sut;

        public FileWritterTests()
        {
            _consoleWrapperMock = new Mock<IConsoleWrapper>();
            _fileWrapperMock = new Mock<IFileWrapper>();
            _sut = new FileWritter(_consoleWrapperMock.Object, _fileWrapperMock.Object);
        }

        [Fact]
        public void SavingFileShouldSucceed()
        {
            var traingle = new List<List<int>> { new List<int> { 3 }, new List<int> { 2, 4 }, new List<int> { 7, 8, 99 } };
            var fileName = "aaa.txt";

            _sut.Save(traingle, fileName);

            _consoleWrapperMock.Verify(x => x.WriteLine(It.Is<string>(y => y.Contains("aaa.txt"))), Times.Once);
            _fileWrapperMock.Verify(x => x.WriteAllLines(fileName, It.IsAny<IEnumerable<string>>()), Times.Once);
        }
    }
}
