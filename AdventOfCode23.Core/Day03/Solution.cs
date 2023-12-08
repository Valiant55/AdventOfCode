using AdventOfCode23.Core.Common;
using System.IO;

namespace AdventOfCode23.Core.Day03;

public class Solution : ISolution
{
    public List<PartNo> Parts { get; set; }
    public List<Position> Gears { get; set; }
    public char[][] Grid { get; set; }

    public static readonly string symbols = "!@#$%^&*()_+={}[];:<>,?/|-";

    public Solution(
        IParser<IEnumerable<PartNo>> partParser,
        IParser<IEnumerable<Position>> gearParser,
        IParser<IEnumerable<char[]>> gridParser)
    {
        var input = @"Day03\input.txt";

        Parts = partParser.Parse(input).ToList();
        Gears = gearParser.Parse(input).ToList();
        Grid = gridParser.Parse(input).ToArray();
    }

    public override long Part01()
    {
        List<int> validParts = new List<int>();

        foreach (var part in Parts)
        {
            bool valid = part.Neighbors.Any(p => {
                try
                {
                    return symbols.Contains(Grid[p.Y][p.X]);
                }
                catch
                {
                    return false;
                }
            });
            if (valid) validParts.Add(part.Id);
        }

        return validParts.Sum();
    }

    public override long Part02()
    {
        int answer = 0;

        foreach (var gear in Gears)
        {
            List<int> ratio = Parts
                .Where(p => p.Neighbors.Contains(gear))
                .Select(p => p.Id)
                .ToList();

            if (ratio.Count == 2) answer += ratio[0] * ratio[1];
        }

        return answer;
    }
}
