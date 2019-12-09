using System;

namespace FutureFinancingTriangle.Generator.Wrappers
{
    internal interface IConsoleWrapper
    {
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        void Write(string s);
        void WriteLine(string s);
    }

    internal class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string s)
        {
            Console.Write(s);
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
