using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day19;

public class Solution : ISolution
{
    public PartSorter PartSorter { get; set; }

    public Solution(IParser<PartSorter> parser)
    {
        PartSorter = parser.Parse(@"Day19\input.txt");
    }

    public override long Part01()
    {
        return PartSorter.SortParts();
    }

    public override long Part02()
    {
        return PartSorter.FindCombinations();
    }
}
