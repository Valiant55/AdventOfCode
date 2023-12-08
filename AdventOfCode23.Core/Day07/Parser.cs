using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day07;

public class Parser : IParser<IEnumerable<Hand>>
{
    public IEnumerable<Hand> Parse(string inputFile)
    {
        List<Hand> result = new List<Hand>();

        foreach (string line in File.ReadLines(inputFile))
        {
            string[] parts = line.Split(" ");
            var hand = new Hand(parts[0], int.Parse(parts[1]));
            result.Add(hand);
        }

        return result;
    }
}
