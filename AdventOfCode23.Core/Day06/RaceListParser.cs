using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day06;

public class RaceListParser : IParser<IEnumerable<Race>>
{
    private readonly Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

    public IEnumerable<Race> Parse(string inputFile)
    {
        List<Race> result = new List<Race>();
        string[] file = File.ReadAllLines(inputFile);

        var timeMatches = regex.Matches(file[0]);
        var distanceMatches = regex.Matches(file[1]);

        foreach (var (time, distance) in timeMatches.Zip(distanceMatches))
        {
            var newRace = new Race() { Time = int.Parse(time.Value), Distance = int.Parse(distance.Value) };
            result.Add(newRace);
        }

        return result;
    }
}
