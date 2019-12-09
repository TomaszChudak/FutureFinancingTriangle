using FutureFinancingTriangle.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace FutureFinancingTriangle.Input
{
    internal interface IFileReader
    {
        string[] Read();
    }

    internal class FileReader : IFileReader
    {
        private readonly string _inputFilePath;

        public FileReader(IOptions<AppSettings> config)
        {
            _inputFilePath = config.Value.InputFilePath;
        }

        public string[] Read()
        {
            return File.ReadAllLines(_inputFilePath);
        }
    }
}
