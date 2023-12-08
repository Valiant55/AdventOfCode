using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day04;

public class Solution : ISolution
{
    public List<ScratchCard> Cards { get; set; }

    public Solution(IParser<IEnumerable<ScratchCard>> parser)
    {
        Cards = parser.Parse(@"Day04\input.txt").ToList();
    }

    public override long Part01()
    {
        return Cards.Select(c => c.PointValue).Sum();
    }

    public override long Part02()
    {
        List<int> cardCounts = Enumerable.Range(0, Cards.Count).Select(c => 1).ToList();

        foreach (var c in Cards.Select((c, i) => new { Card = c, Index = i }))
        {
            foreach (var i in Enumerable.Range(c.Index + 1, c.Card.Matches))
            {
                cardCounts[i] += cardCounts[c.Index];
            }
        }

        return cardCounts.Sum();
    }

    
}
