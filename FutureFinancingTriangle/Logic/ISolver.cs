using FutureFinancingTriangle.Models;

namespace FutureFinancingTriangle.Logic
{
    internal interface ISolver
    {
        ResultPath Solve(InputTriangle inputTriangle);
    }
}
