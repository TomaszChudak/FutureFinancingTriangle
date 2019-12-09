using System.Linq;

namespace FutureFinancingTriangle.Input
{
    internal interface ILineParser
    {
        int[] Parse(string line);
    }

    internal class LineParser : ILineParser
    {
        public int[] Parse(string line)
        {
            return line.Split(' ').Select(x => int.Parse(x)).ToArray();
        }
    }
}
