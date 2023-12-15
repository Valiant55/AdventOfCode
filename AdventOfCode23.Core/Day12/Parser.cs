using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day12;

public class Parser : IParser<List<SpringReading2>>
{
    public List<SpringReading2> Parse(string inputFile)
    {
        List<SpringReading2> readings = new();

        foreach (string line in File.ReadLines(inputFile))
        {
            string[] parts = line.Split(' ');

            SpringReading2 reading = new SpringReading2(
                parts[0],
                parts[1].Split(',').Select(r => int.Parse(r)).ToList()
            );

            readings.Add(reading);
        }
        
        return readings;
    }
}
