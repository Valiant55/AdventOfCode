using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day14;

public class Solution : ISolution
{
    public Platform Platform { get; set; }

    public Solution(IParser<Platform> parser, string inputFile = @"Day14\input.txt")
    {
        Platform = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        return Platform.TiltAndCalculateLoad();
    }

    public override long Part02()
    {
        return Platform.CycleAndCalculateLoad();
    }
}
