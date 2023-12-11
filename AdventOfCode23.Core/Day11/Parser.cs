using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day11;

public class Parser : IParser<Universe>
{

    public Universe Parse(string inputFile)
    {
        List<List<char>> obs = new();

        foreach((var line, var i) in File.ReadLines(inputFile).Select((l, i) => (l, i)))
        {
            List<char> currentRow = new();
            foreach(char c in line)
            {
                currentRow.Add(c);
            }
            obs.Add(currentRow);
        }

        return new Universe() { Observations = obs.Select(a => a.ToArray()).ToArray()};
    }
}
