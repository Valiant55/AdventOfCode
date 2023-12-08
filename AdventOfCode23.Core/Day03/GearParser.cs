using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Utils;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day03;

public class GearParser : IParser<IEnumerable<Position>>
{
    private readonly Regex regex = new Regex(@"\*", RegexOptions.Compiled);

    public IEnumerable<Position> Parse(string inputFile)
    {
        List<Position> gears = new List<Position>();

        foreach (var line in File.ReadLines(inputFile).Select((l, i) => new { Value = l, Index = i }))
        {
            var matches = regex.Matches(line.Value);

            foreach (Match match in matches)
            {
                var pos = new Position(match.Index, line.Index);
                gears.Add(pos);
            }
        }

        return gears;
    }
}
