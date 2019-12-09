using FutureFinancingTriangle.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FutureFinancingTriangle.Logic
{
    internal interface ISolverFactory
    {
        ISolver CreateSolver();
    }

    internal class SolverFactory : ISolverFactory
    {
        private enum SolverKinds { Graph, Array }

        private readonly string _solverKind;
        private readonly IServiceProvider _serviceProvider;

        public SolverFactory(IOptions<AppSettings> config, IServiceProvider serviceProvider)
        {
            _solverKind = config.Value.SolverKind;
            _serviceProvider = serviceProvider;
        }

        public ISolver CreateSolver()
        {
            var solverKind = GetSolverKind(_solverKind);

            switch (solverKind)
            {
                case SolverKinds.Array: return new ArraySolver();
                case SolverKinds.Graph: return new GraphSolver(_serviceProvider.GetService<IGraphCreator>());
                default: throw new ApplicationException("Wrong value of SolverKind");
            }
        }

        private SolverKinds GetSolverKind(string solverKind)
        {
            switch(solverKind)
            {
                case "Array": return SolverKinds.Array;
                case "Graph": return SolverKinds.Graph;
                default: throw new ApplicationException("Wrong value of SolverKind node in settings file. Allowed values: Array or Graph");
            }
        }
    }
}
