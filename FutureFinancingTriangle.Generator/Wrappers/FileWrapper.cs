using System.Collections.Generic;
using System.IO;

namespace FutureFinancingTriangle.Generator.Wrappers
{
    internal interface IFileWrapper
    {
        void WriteAllLines(string path, IEnumerable<string> contents);
    }

    internal class FileWrapper : IFileWrapper
    {
        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            File.WriteAllLines(path, contents);
        }
    }
}
