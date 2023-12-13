using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day12;

public class Parser : IParser<List<SpringReading>>
{
    public List<SpringReading> Parse(string inputFile)
    {
        List<SpringReading> readings = new();

        foreach (string line in File.ReadLines(inputFile))
        {
            string[] parts = line.Split(' ');

            SpringReading reading = new SpringReading(
                parts[0],
                parts[1].Split(',').Select(r => int.Parse(r)).ToList()
            );

            readings.Add(reading);
        }
        
        return readings;
    }
}
