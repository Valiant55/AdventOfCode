﻿using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day01;
using AdventOfCode23.Core.Day02;
using AdventOfCode23.Core.Day03;
using AdventOfCode23.Core.Day04;
using AdventOfCode23.Core.Day05;
using AdventOfCode23.Core.Day06;
using AdventOfCode23.Core.Day07;
using AdventOfCode23.Core.Day10;
using AdventOfCode23.Core.Day11;
using AdventOfCode23.Core.Day12;
using AdventOfCode23.Core.Day13;
using AdventOfCode23.Core.Day14;
using AdventOfCode23.Core.Day15;
using AdventOfCode23.Core.Day16;
using AdventOfCode23.Core.Day17;
using AdventOfCode23.Core.Day18;
using AdventOfCode23.Core.Day19;
using AdventOfCode23.Core.Day20;
using AdventOfCode23.Core.Day21;
using AdventOfCode23.Core.Day22;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode23.Core;

public static class DependecyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        /* Day01 */
        services.AddTransient<IParser<List<string>>, Day01.Parser>();
        services.AddTransient<ISolution, Day01.Solution>();

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

        /* Day08 */
        services.AddTransient<IParser<Day08.Map>, Day08.Parser>();
        services.AddTransient<ISolution, Day08.Solution>();

        /* Day09 */
        services.AddTransient<IParser<List<Day09.Reading>>, Day09.Parser>();
        services.AddTransient<ISolution, Day09.Solution>();

        /* Day10 */
        services.AddTransient<IParser<PipeMap>, Day10.Parser>();
        services.AddTransient<ISolution, Day10.Solution>();

        /* Day11 */
        services.AddTransient<IParser<Universe>, Day11.Parser>();
        services.AddTransient<ISolution, Day11.Solution>();

        /* Day12 */
        services.AddKeyedTransient<IParser<List<SpringReading>>, Day12.Parser>("day12_part01");
        services.AddKeyedTransient<IParser<List<SpringReading>>, Day12.Parser5x>("day12_part02");
        services.AddTransient<ISolution, Day12.Solution>();

        /* Day13 */
        services.AddTransient<IParser<List<LavaMap>>, Day13.Parser>();
        services.AddTransient<ISolution, Day13.Solution>();

        /* Day14 */
        services.AddTransient<IParser<Platform>, Day14.Parser>();
        services.AddTransient<ISolution, Day14.Solution>();

        /* Day15 */
        services.AddTransient<IParser<List<Step>>, Day15.Parser>();
        services.AddTransient<ISolution, Day15.Solution>();

        /* Day16 */
        services.AddTransient<IParser<MirrorArray>, Day16.Parser>();
        services.AddTransient<ISolution, Day16.Solution>();

        /* Day17 */
        services.AddTransient<IParser<GearCity>, Day17.Parser>();
        services.AddTransient<ISolution, Day17.Solution>();

        /* Day18 */
        services.AddTransient<IParser<LavaLagoon>, Day18.Parser>();
        services.AddTransient<ISolution, Day18.Solution>();

        /* Day19 */
        services.AddTransient<IParser<PartSorter>, Day19.Parser>();
        services.AddTransient<ISolution, Day19.Solution>();

        /* Day20 */
        services.AddTransient<IParser<Machines>, Day20.Parser>();
        services.AddTransient<ISolution, Day20.Solution>();

        /* Day21 */
        services.AddTransient<IParser<Garden>, Day21.Parser>();
        services.AddTransient<ISolution, Day21.Solution>();

        return services;
    }
}
