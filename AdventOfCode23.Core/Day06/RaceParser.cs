using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day06;

public class RaceParser : IParser<Race>
{
    private readonly Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

    public Race Parse(string inputFile)
    {
        Race result = new Race();
        string[] file = File.ReadAllLines(inputFile)
            .Select(l => l.Replace(" ", ""))
            .ToArray();

        var timeMatches = regex.Matches(file[0]);
        var distanceMatches = regex.Matches(file[1]);

        return new Race { Time = long.Parse(timeMatches[0].Value), Distance = long.Parse(distanceMatches[0].Value) };
    }
}
