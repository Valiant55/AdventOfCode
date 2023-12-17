using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day17;

public class Solution : ISolution
{
    GearCity GearCity { get; set; }

    public Solution(IParser<GearCity> parser)
    {
        GearCity = parser.Parse(@"Day17\input.txt");
    }

    public override long Part01()
    {
        return GearCity.FindShortestPath();
    }

    public override long Part02()
    {
        return 0;
    }
}
