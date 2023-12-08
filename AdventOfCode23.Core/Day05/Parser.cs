using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day05;

public class Parser : IParser<Almanac>
{
    private readonly Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

    public Almanac Parse(string inputFile)
    {
        var lines = File.ReadLines(inputFile).ToList();
        var result = new Almanac();
        var maps = new List<Map>();
        
        foreach(Match match in regex.Matches(lines.First()))
        {
            result.Seeds.Add(new Seed() { Id = long.Parse(match.Value) });
        }

        var map = new Map();
        var skipped = false;
        foreach (var line in File.ReadLines(inputFile).Skip(3))
        {
            var matches = regex.Matches(line);
            if(matches.Count == 0 && !skipped)
            {
                skipped = true;
                maps.Add(map);
                map = new Map();
                continue;
            }
            else if (matches.Count == 3)
            {
                skipped = false;
                map.DestinationStart.Add(long.Parse(matches[0].Value));
                map.SourceStart.Add(long.Parse(matches[1].Value));
                map.Length.Add(long.Parse(matches[2].Value));
            }
            else if(matches.Count == 0 && skipped)
            {
                continue;
            }
            else
            {
                throw new Exception("Invalid input file format.");
            }
        }

        maps.Add(map);

        result.SeedToSoilMap = maps[0];
        result.SoilToFertilizerMap = maps[1];
        result.FertilzerToWaterMap = maps[2];
        result.WaterToLightMap = maps[3];
        result.LightToTemperature = maps[4];
        result.TemperatureToHumidityMap = maps[5];
        result.HumidityToLocationMap = maps[6];

        return result;
    }
}
