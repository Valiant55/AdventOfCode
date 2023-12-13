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

            string foldedReading = string.Join("?",Enumerable.Repeat(parts[0], 5));
            string foldedConditions = string.Join(",", Enumerable.Repeat(parts[1], 5));

            SpringReading reading = new SpringReading(
                foldedReading,
                foldedConditions.Split(',').Select(r => int.Parse(r)).ToList()
            );

            readings.Add(reading);
        }
        
        return readings;
    }
}
