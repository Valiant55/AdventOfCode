using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day16;

public class Solution : ISolution
{
    private readonly IParser<MirrorArray> _parser;

    public Solution(IParser<MirrorArray> parser)
    {
        _parser = parser;
    }

    public override long Part01()
    {
        MirrorArray array = _parser.Parse(@"Day16\input.txt");
        return array.CountTraveledNodes();
    }

    public override long Part02()
    {
        return 0;
    }
}
