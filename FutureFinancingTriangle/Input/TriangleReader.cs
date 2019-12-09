using FutureFinancingTriangle.Models;
using System;
using System.Collections.Generic;

namespace FutureFinancingTriangle.Input
{
    internal interface ITriangleReader
    {
        InputTriangle Read();
    }

    internal class TriangleReader : ITriangleReader
    {
        private readonly IFileReader _fileReader;
        private readonly ILineParser _lineParser;

        public TriangleReader(IFileReader fileReader, ILineParser lineParser)
        {
            _fileReader = fileReader;
            _lineParser = lineParser;
        }

        public InputTriangle Read()
        {
            var fileLines = _fileReader.Read();
            var linesParsed = new List<int[]>();

            for (int i = 0; i < fileLines.Length; i++)
            {
                try
                {
                    linesParsed.Add(_lineParser.Parse(fileLines[i]));
                }
                catch
                {
                    throw new FormatException($"Exception during parsing input file -> line number: {i + 1}");
                }
            }            

            return new InputTriangle { Levels = linesParsed.ToArray() };
        }
    }
}
