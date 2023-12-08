using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day01;
using AdventOfCode23.Core.Day02;
using AdventOfCode23.Core.Day03;
using AdventOfCode23.Core.Day04;
using AdventOfCode23.Core.Day05;
using AdventOfCode23.Core.Day06;
using AdventOfCode23.Core.Day07;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode23.Core;

public static class DependecyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        /* Day03 */
        services.AddTransient<IParser<IEnumerable<PartNo>>, Day03.PartNoParser>();
        services.AddTransient<IParser<IEnumerable<Position>>, Day03.GearParser>();
        services.AddTransient<IParser<IEnumerable<char[]>>, Day03.EngineParser>();
        services.AddTransient<ISolution, Day03.Solution>();

        /* Day04 */
        services.AddTransient<IParser<IEnumerable<ScratchCard>>, Day04.Parser>();
        services.AddTransient<ISolution, Day04.Solution>();

        /* Day05 */
        services.AddTransient<IParser<Almanac>, Day05.Parser>();
        services.AddTransient<ISolution, Day05.Solution>();

        /* Day06 */
        services.AddTransient<IParser<IEnumerable<Race>>, Day06.RaceListParser>();
        services.AddTransient<IParser<Race>, Day06.RaceParser>();
        services.AddTransient<ISolution, Day06.Solution>();

        /* Day07 */
        services.AddTransient<IParser<IEnumerable<Hand>>, Day07.Parser>();
        services.AddTransient<ISolution, Day07.Solution>();

        return services;
    }
}
