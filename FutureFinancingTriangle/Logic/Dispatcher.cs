using FutureFinancingTriangle.Input;
using FutureFinancingTriangle.Output;
using System;

namespace FutureFinancingTriangle.Logic
{
    internal interface IDispatcher
    {
        void Proceed();
    }

    internal class Dispatcher : IDispatcher
    {
        private readonly IErrorDisplayer _errorDisplayer;
        private readonly IResultDisplayer _resultDisplayer;
        private readonly ISolverFactory _solverFactory;
        private readonly ITriangleChecker _triangleChecker;
        private readonly ITriangleReader _triangleReader;        

        public Dispatcher(IErrorDisplayer errorDisplayer, IResultDisplayer resultDisplayer, ISolverFactory solverFactory, ITriangleChecker triangleChecker, ITriangleReader triangleReader)
        {
            _errorDisplayer = errorDisplayer;
            _resultDisplayer = resultDisplayer;
            _solverFactory = solverFactory;
            _triangleChecker = triangleChecker;
            _triangleReader = triangleReader;            
        }

        public void Proceed()
        {
            try
            {
                TryProceed();
            }
            catch (Exception ex)
            {
                _errorDisplayer.Display(ex.Message);
            }
        }

        private void TryProceed()
        {
            var inputTriangle = _triangleReader.Read();

            if (!_triangleChecker.Check(inputTriangle, out var error))
            {
                _errorDisplayer.Display(error);
                return;
            }

            var solver = _solverFactory.CreateSolver();

            var startTime = DateTime.Now;

            var resultPath = solver.Solve(inputTriangle);

            resultPath.SolvingTime = DateTime.Now - startTime;

            _resultDisplayer.Display(inputTriangle, resultPath);
        }
    }
}
