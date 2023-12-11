using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day10;

public class Solution : ISolution
{
    public PipeMap PipeMap { get; set; }

    public Solution(IParser<PipeMap> parser, string inputFile = @"Day10\input.txt")
    {
        PipeMap = parser.Parse(inputFile);
    }

    public override long Part01()
    {
        return PipeMap.FindMaxDistance();
    }

    public override long Part02()
    {
        return PipeMap.FindInclosedArea();
    }
}
