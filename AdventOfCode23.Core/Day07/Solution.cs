using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day07;

public class Solution : ISolution
{
    public List<Hand> Hands { get; set; }

    public Solution(IParser<IEnumerable<Hand>> parser, string inputFile = @"Day07\input.txt")
    {
        Hands = parser.Parse(inputFile).ToList();
    }

    public override long Part01()
    {
        Hands.Sort();
        return Hands.Select((c, i) => c.Bid * (i+1)).Sum();
    }

    public override long Part02()
    {
        Hands.ForEach(h => h.UseWildJacks());
        Hands.Sort();
        return Hands.Select((c, i) => c.Bid * (i + 1)).Sum();
    }
}
