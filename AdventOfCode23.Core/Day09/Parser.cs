using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day09;

public class Parser : IParser<List<Reading>>
{
    private static readonly Regex regex = new Regex(@"(\-)?\d+", RegexOptions.Compiled);
    public List<Reading> Parse(string inputFile)
    {
        var result = new List<Reading>();

        foreach(var line in File.ReadLines(inputFile))
        {
            var reading = new Reading();
            var matches = regex.Matches(line);

            foreach(Match m in matches)
            {
                reading.Readings.Add(long.Parse(m.Value));
            }

            result.Add(reading);
        }

        return result;
    }
}
