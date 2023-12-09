using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day08;

public class Solution : ISolution
{
    public Map Map { get; set; }

    public Solution(IParser<Map> parser, string inputFile = @"Day08\input.txt")
    {
        Map = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        return Map.StepsRequired();
    }

    public override long Part02()
    {
        return Map.GhostStepsRequired();
    }
}
