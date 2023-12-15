using AdventOfCode23.Core;
using AdventOfCode23.Core.Common;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddCoreServices()
    .BuildServiceProvider();

var solutions = serviceProvider
    .GetServices<ISolution>()
    .TakeLast(1);

foreach (var solution in solutions)
{
    Console.WriteLine(solution.ToString());
}
