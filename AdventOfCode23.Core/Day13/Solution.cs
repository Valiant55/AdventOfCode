using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day13;

public class Solution : ISolution
{
    public List<LavaMap> LavaMaps { get; set; }

    public Solution(IParser<List<LavaMap>> parser, string inputFile = @"Day13\input.txt")
    {
        LavaMaps = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        return LavaMaps.Select(m => m.FindReflectionValues()).Sum();
    }

    public override long Part02()
    {
        return 0;
    }
}
