using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day16;

public class Solution : ISolution
{
    public MirrorArray Array { get; set; }

    public Solution(IParser<MirrorArray> parser)
    {
        Array = parser.Parse(@"Day16\input.txt");
    }

    public override long Part01()
    {
        return Array.CountTraveledNodes();
    }

    public override long Part02()
    {
        return Array.FindMaxEnergizedTiles();
    }
}
