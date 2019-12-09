using FutureFinancingTriangle.Input;
using Moq;
using System;
using Xunit;

namespace Future.Financing.Triangle.Tests
{
    public class TriangleReaderTests
    {
        private readonly Mock<IFileReader> _fileReaderMock;
        private readonly Mock<ILineParser> _lineParserMock;
        private readonly TriangleReader _sut;

        public TriangleReaderTests()
        {
            _fileReaderMock = new Mock<IFileReader>();
            _lineParserMock = new Mock<ILineParser>();
            _sut = new TriangleReader(_fileReaderMock.Object, _lineParserMock.Object);
        }

        [Fact]
        public void ReadsTriangle_When_InputTriangleIsGood()
        {
            _fileReaderMock.Setup(x => x.Read())
                .Returns(new string[] { "1", "2 3", "4 5 6" });
            _lineParserMock.Setup(x => x.Parse(It.IsAny<string>()))
                .Returns(new int[] { 1 });

            var result = _sut.Read();

            Assert.Equal(3, result.Levels.Length);
            Assert.All<int[]>(result.Levels, (x) => { Assert.Equal(new int[] { 1 }, x); });
        }

        [Fact]
        public void ThrowsException_When_ParsingThrowsException()
        {
            _fileReaderMock.Setup(x => x.Read())
                .Returns(new string[] { "1", "2 3", "4 5 6" });
            _lineParserMock.Setup(x => x.Parse(It.IsAny<string>()))
                .Throws<FormatException>();

            Action a = () => _sut.Read();

            Assert.Throws<FormatException>(a);
        }
    }
}
