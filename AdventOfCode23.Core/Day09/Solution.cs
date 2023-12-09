using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day09;

public class Solution : ISolution
{
    public List<Reading> Readings { get; set; }

    public Solution(IParser<List<Reading>> parser, string inputFile = @"Day09/input.txt")
    {
        Readings = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        var results = Readings.Select(r => r.PredicatedNextReadings().Last());
        return results.Sum();
    }

    public override long Part02()
    {
        var results = Readings.Select(r => r.PredicatedPreviousReadings().Last());
        return results.Sum();
    }
}
