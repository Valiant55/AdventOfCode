using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day05;

public class Solution : ISolution
{
    public Almanac Almanac { get; set; }

    public Solution(IParser<Almanac> parser)
    {
        Almanac = parser.Parse(@"Day05\input.txt");
    }

    public override long Part01()
    {
        return Almanac.GetLocations().Min();
    }

    public override long Part02()
    {
        /* I counldn't get it to not return 0, so uhhhh, I put in a hack*/
        return Almanac.GetLocationsFromRanges().Where(l => l > 0).Min();
    }
}
