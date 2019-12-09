using FutureFinancingTriangle.Generator.Input;
using FutureFinancingTriangle.Generator.Logic;
using FutureFinancingTriangle.Generator.Output;
using FutureFinancingTriangle.Generator.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("FutureFinancingTriangle.Generator.Tests")]

namespace FutureFinancingTriangle.Generator
{
    class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = GetServiceCollection();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var manager = serviceProvider.GetService<IManager>();

            manager.DoIt();
        }

        private static IServiceCollection GetServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IConsoleReader, ConsoleReader>();
            serviceCollection.AddScoped<ITriangleGenerator, TriangleGenerator>();
            serviceCollection.AddScoped<IManager, Manager>();
            serviceCollection.AddScoped<IRandomser, Randomiser>();
            serviceCollection.AddScoped<IFileWritter, FileWritter>();
            serviceCollection.AddScoped<IConsoleWrapper, ConsoleWrapper>();
            serviceCollection.AddScoped<IFileWrapper, FileWrapper>();

            return serviceCollection;
        }
    }
}
