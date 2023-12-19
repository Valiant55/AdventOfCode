using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day18;

public class Solution : ISolution
{
    public LavaLagoon Lagoon { get; set; }

    public Solution(IParser<LavaLagoon> parser, string inputFile = @"Day18\input.txt")
    {
        Lagoon = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        return Lagoon.CountPitArea();
    }

    public override long Part02()
    {
        return Lagoon.CountCorrectedPitArea();
    }
}
