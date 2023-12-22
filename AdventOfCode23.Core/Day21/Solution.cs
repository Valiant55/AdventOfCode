using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day22;

namespace AdventOfCode23.Core.Day21;

public class Solution : ISolution
{
    private readonly IParser<Garden> _parser;
    private string InputFile { get; set; }

    public Solution(IParser<Garden> parser, string inputFile = @"Day21\input.txt")
    {
        _parser = parser;
        InputFile = inputFile;
    }

    public override long Part01()
    {
        var garden = _parser.Parse(InputFile);
        return garden.CountPlots();
    }

    public override long Part02()
    {
        return 0;
    }
}
