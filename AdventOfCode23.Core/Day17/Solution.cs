using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day17;

public class Solution : ISolution
{
    GearCity GearCity { get; set; }

    public Solution(IParser<GearCity> parser, string inputFile = @"Day17\input.txt")
    {
        GearCity = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        return GearCity.FindShortestPath();
    }

    public override long Part02()
    {
        return GearCity.FindShortestPathUltra();
    }
}
