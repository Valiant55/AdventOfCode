using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day15;

public class Solution : ISolution
{
    public List<Step> Steps { get; set; }
    public BoxArray BoxArray { get; set; }

    public Solution(IParser<List<Step>> parser)
    {
        Steps = parser.Parse(@"Day15\input.txt");
    }

    public override long Part01()
    {
        return Steps.Select(s => s.Value.ApplyHASH()).Sum();
    }

    public override long Part02()
    {
        BoxArray = new BoxArray();
        BoxArray.BuildArray(Steps);
        return BoxArray.CalculateFocusingPower();
    }
}
