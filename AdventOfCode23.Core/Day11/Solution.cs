using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day11;

public class Solution : ISolution
{
    public Universe Universe { get; set; }

    public Solution(IParser<Universe> parser)
    {
        Universe = parser.Parse(@"Day11\input.txt");
    }

    public override long Part01()
    {
        return Universe.FindDistances();
    }

    public override long Part02()
    {
        return 0;
    }
}
