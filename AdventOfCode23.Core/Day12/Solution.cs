using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day12;

public class Solution : ISolution
{
    public List<SpringReading> SpringReadings { get; set; }

    public Solution(IParser<List<SpringReading>> parser)
    {
        SpringReadings = parser.Parse(@"Day12\input.txt");
    }

    public override long Part01()
    {
        return SpringReadings.Select(r => r.CountArrangements()).Sum();
    }

    public override long Part02()
    {
        return 0;
    }
}
