using FutureFinancingTriangle.Configuration;
using FutureFinancingTriangle.Logic;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Configuration;
using FutureFinancingTriangle.Output;
using FutureFinancingTriangle.Input;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("FutureFinancingTriangle.Tests")]

namespace FutureFinancingTriangle
{
    class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = GetServiceCollection();

            SetConfiguration(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var dispatcher = serviceProvider.GetService<IDispatcher>();

            dispatcher.Proceed();
        }

        private static void SetConfiguration(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration);
        }

        private static IServiceCollection GetServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IConsoleWrapper, ConsoleWrapper>();
            serviceCollection.AddScoped<IDispatcher, Dispatcher>();
            serviceCollection.AddScoped<IErrorDisplayer, ErrorDisplayer>();
            serviceCollection.AddScoped<IFileReader, FileReader>();
            serviceCollection.AddScoped<IGraphCreator, GraphCreator>();
            serviceCollection.AddScoped<ILineParser, LineParser>();
            serviceCollection.AddScoped<IResultDisplayer, ResultDisplayer>();
            serviceCollection.AddScoped<ISolverFactory, SolverFactory>();
            serviceCollection.AddScoped<ITriangleChecker, TriangleChecker>();
            serviceCollection.AddScoped<ITriangleReader, TriangleReader>();

            return serviceCollection;
        }
    }
}
