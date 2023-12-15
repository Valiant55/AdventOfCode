﻿using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day09;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode23.Core.Day12;

public class Solution : ISolution
{
    private readonly IParser<List<SpringReading2>> _part01;
    private readonly IParser<List<SpringReading>> _part02;

    public Solution(
        [FromKeyedServices("day12_part01")] IParser<List<SpringReading2>> part01,
        [FromKeyedServices("day12_part02")] IParser<List<SpringReading>> part02)
    {
        _part01 = part01;
        _part02 = part02;
    }

    public override long Part01()
    {
        var SpringReadings = _part01.Parse(@"Day12\input.txt");
        var answer = SpringReadings.Select(r => r.CountArrangements()).ToList();
        Console.WriteLine($"Total Hits: {SpringReading.CacheHit}");
        Console.WriteLine($"Total Misses: {SpringReading.CacheMiss}");
        Console.WriteLine($"Total Combos: {SpringReading.Cache.Count}");
        return answer.Sum();
    }

    public override long Part02()
    {
        /*
        var SpringReadings = _part02.Parse(@"Day12\input.txt");
        long answer = SpringReadings.Select(r => r.CountArrangements()).Sum();
        Console.WriteLine($"Total Hits: {SpringReading.CacheHit}");
        Console.WriteLine($"Total Misses: {SpringReading.CacheMiss}");
        Console.WriteLine($"Total Combos: {SpringReading.Cache.Count}");*/
        return 0;
    }
}
