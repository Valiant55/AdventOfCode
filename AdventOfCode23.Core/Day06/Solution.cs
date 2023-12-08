using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day06;

public class Solution : ISolution
{
    public List<Race> Races { get; set; }
    public Race Race { get; set; }

    public Solution(IParser<IEnumerable<Race>> parser, IParser<Race> singleRace)
    {
        Races = parser is not null ? parser.Parse(@"Day06\input.txt").ToList() : new List<Race>();
        Race = singleRace is not null ? singleRace.Parse(@"Day06\input.txt") : new Race();
    }

    public override long Part01()
    {
        return Races.Select(r => r.PossibleWins()).Aggregate((a, w) => a * w);
    }

    public override long Part02()
    {
        return Race.PossibleWins();
    }
}
