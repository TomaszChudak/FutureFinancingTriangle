using FutureFinancingTriangle.Generator.Input;
using FutureFinancingTriangle.Generator.Output;
using FutureFinancingTriangle.Generator.Wrappers;

namespace FutureFinancingTriangle.Generator.Logic
{
    internal interface IManager
    {
        void DoIt();
    }

    internal class Manager : IManager
    {
        private IConsoleReader _consoleReader;
        private IConsoleWrapper _consoleWrapper;
        private ITriangleGenerator _triangleGenerator;
        private IFileWritter _fileWritter;

        public Manager(IConsoleReader consoleReader, IConsoleWrapper consoleWrapper, ITriangleGenerator triangleGenerator, IFileWritter fileWritter)
        {
            _consoleReader = consoleReader;
            _consoleWrapper = consoleWrapper;
            _triangleGenerator = triangleGenerator;
            _fileWritter = fileWritter;
        }

        public void DoIt()
        {
            var numberOfLevels = _consoleReader.ReadPositiveIntFromConsole("Enter number of levels");
            var maxNumber = _consoleReader.ReadPositiveIntFromConsole("Enter max possible value");
            var oddEvenMix = _consoleReader.ReadBoolFromConsole("Do you want subsequent odd/even layers [y/n]");
            var fileName = _consoleReader.ReadStringFromConsole("Enter output file name");

            var generatedTriangle = _triangleGenerator.Generate(numberOfLevels, maxNumber, oddEvenMix);
            _fileWritter.Save(generatedTriangle, fileName);

            _consoleWrapper.WriteLine("Press any key to quit");
            _consoleWrapper.ReadKey();
        }
    }
}
