namespace FutureFinancingTriangle.Output
{
    internal interface IErrorDisplayer
    {
        void Display(string error);
    }

    internal class ErrorDisplayer : IErrorDisplayer
    {
        private readonly IConsoleWrapper _consoleWrapper;

        public ErrorDisplayer(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public void Display(string error)
        {
            _consoleWrapper.WriteLine(error);
            _consoleWrapper.WriteLine("Press any key to quit");
            _consoleWrapper.ReadKey();
        }
    }
}
