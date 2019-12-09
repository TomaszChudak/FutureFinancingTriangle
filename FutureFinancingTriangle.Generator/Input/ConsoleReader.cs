using FutureFinancingTriangle.Generator.Wrappers;

namespace FutureFinancingTriangle.Generator.Input
{
    internal interface IConsoleReader
    {
        bool ReadBoolFromConsole(string prompt);
        int ReadPositiveIntFromConsole(string prompt);
        string ReadStringFromConsole(string prompt);
    }

    internal class ConsoleReader : IConsoleReader
    {
        private IConsoleWrapper _consoleWrapper;

        public ConsoleReader(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public bool ReadBoolFromConsole(string prompt)
        {
            while (true)
            {
                _consoleWrapper.Write($"{prompt} > ");
                var text = _consoleWrapper.ReadLine();

                if (text.Trim().ToLowerInvariant() == "y")
                    return true;
                if (text.Trim().ToLowerInvariant() == "n")
                    return false;                

                _consoleWrapper.WriteLine("Please enter 'y' or 'n' only!");
            }
        }

        public int ReadPositiveIntFromConsole(string prompt)
        {
            while (true)
            {
                _consoleWrapper.Write($"{prompt} > ");
                var text = _consoleWrapper.ReadLine();

                if (!int.TryParse(text, out var number))
                {
                    _consoleWrapper.WriteLine("Please enter integer value!");
                    continue;
                }

                if (number <= 1)
                {
                    _consoleWrapper.WriteLine("Please enter integer value greater than 1!");
                    continue;
                }

                return number;
            }
        }

        public string ReadStringFromConsole(string prompt)
        {
            _consoleWrapper.Write($"{prompt} > ");
            return _consoleWrapper.ReadLine();
        }
    }
}
