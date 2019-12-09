using FutureFinancingTriangle.Generator.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace FutureFinancingTriangle.Generator.Output
{
    internal interface IFileWritter
    {
        void Save(IEnumerable<IEnumerable<int>> generatedTriangle, string fileName);
    }

    internal class FileWritter : IFileWritter
    {
        private IConsoleWrapper _consoleWrapper;
        private IFileWrapper _fileWrapper;

        public FileWritter(IConsoleWrapper consoleWrapper, IFileWrapper fileWrapper)
        {
            _consoleWrapper = consoleWrapper;
            _fileWrapper = fileWrapper;
        }

        public void Save(IEnumerable<IEnumerable<int>> generatedTriangle, string fileName)
        {
            var lines = generatedTriangle.Select(x => string.Join(' ', x));
            _fileWrapper.WriteAllLines(fileName, lines);
            _consoleWrapper.WriteLine($"Triangle has been saved to {fileName}");
        }
    }
}
