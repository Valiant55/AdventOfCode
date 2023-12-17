﻿using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day17;

public class Solution : ISolution
{
    List<string> SolutionData { get; set; }

    public Solution(IParser<List<string>> parser)
    {
        SolutionData = parser.Parse(@"Day17\input.txt");
    }

    public override long Part01()
    {
        return 0;
    }

    public override long Part02()
    {
        return 0;
    }
}
