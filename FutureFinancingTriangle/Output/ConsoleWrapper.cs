using System;

namespace FutureFinancingTriangle.Output
{
    internal interface IConsoleWrapper
    {
        ConsoleColor ForegroundColor { set; }
        ConsoleKeyInfo ReadKey();
        void ResetColor();
        void Write(string s);
        void WriteLine();
        void WriteLine(string s);
    }

    internal class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleColor ForegroundColor { set => Console.ForegroundColor = value; }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void Write(string s)
        {
            Console.Write(s);
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
