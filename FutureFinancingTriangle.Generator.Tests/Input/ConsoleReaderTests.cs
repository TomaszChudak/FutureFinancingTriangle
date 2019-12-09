using FutureFinancingTriangle.Generator.Input;
using FutureFinancingTriangle.Generator.Wrappers;
using Moq;
using Xunit;

namespace FutureFinancingTriangle.Generator.Tests
{
    public class ConsoleReaderTests
    {
        private readonly Mock<IConsoleWrapper> _consoleWrapperMock;
        private readonly IConsoleReader _sut;

        public ConsoleReaderTests()
        {
            _consoleWrapperMock = new Mock<IConsoleWrapper>();
            _sut = new ConsoleReader(_consoleWrapperMock.Object);
        }

        [Fact]
        public void ReadBoolFromConsoleReturnsFalse_When_NKeyPressed()
        {
            _consoleWrapperMock.Setup(x => x.ReadLine())
                .Returns("n");

            var result = _sut.ReadBoolFromConsole("aaa");

            Assert.False(result);

            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(z => z.Contains("aaa"))), Times.Once);            
        }

        [Fact]
        public void ReadBoolFromConsoleReturnsTrue_When_YKeyPressed()
        {
            _consoleWrapperMock.Setup(x => x.ReadLine())
                .Returns("y");

            var result = _sut.ReadBoolFromConsole("aaa");

            Assert.True(result);

            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(z => z.Contains("aaa"))), Times.Once);
        }

        [Fact]
        public void ReadBoolFromConsoleReturnsTrue_When_WrongKeyPressedAndYKeyPressed()
        {
            _consoleWrapperMock.SetupSequence(x => x.ReadLine())
                .Returns("x")
                .Returns("y");

            var result = _sut.ReadBoolFromConsole("aaa");

            Assert.True(result);

            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(z => z.Contains("aaa"))), Times.Exactly(2));
        }

        [Theory]
        [InlineData("10", 10)]
        [InlineData("20", 20)]
        [InlineData("200", 200)]
        public void ReadPositiveIntFromConsoleReturnsNumber_When_RightNumberEntered(string enteredNumber, int expectedResult)
        {
            _consoleWrapperMock.Setup(x => x.ReadLine())
                .Returns(enteredNumber);

            var result = _sut.ReadPositiveIntFromConsole("bbb");

            Assert.Equal(expectedResult, result);

            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(z => z.Contains("bbb"))), Times.Once);
        }

        [Theory]
        [InlineData("1", "10", 10)]
        [InlineData("-1", "20", 20)]
        [InlineData("0", "200", 200)]
        [InlineData("zzz", "200", 200)]
        public void ReadPositiveIntFromConsoleReturnsNumber_When_WrongNumberEnteredAtFirstTime(string wrongNumber, string rightNumber, int expectedResult)
        {
            _consoleWrapperMock.SetupSequence(x => x.ReadLine())
                .Returns(wrongNumber)
                .Returns(rightNumber);

            var result = _sut.ReadPositiveIntFromConsole("bbbb");

            Assert.Equal(expectedResult, result);

            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(z => z.Contains("bbbb"))), Times.Exactly(2));
        }

        [Theory]
        [InlineData("aaa")]
        [InlineData("-1")]
        [InlineData("ttt")]
        [InlineData("")]
        public void ReadStringFromConsoleReturnsString(string text)
        {
            _consoleWrapperMock.SetupSequence(x => x.ReadLine())
                .Returns(text);

            var result = _sut.ReadStringFromConsole("ccc");

            Assert.Equal(text, result);

            _consoleWrapperMock.Verify(x => x.Write(It.Is<string>(z => z.Contains("ccc"))), Times.Once);
        }
    }
}
