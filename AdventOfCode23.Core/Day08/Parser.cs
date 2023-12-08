using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;
using static AdventOfCode23.Core.Day08.Map;

namespace AdventOfCode23.Core.Day08;

public class Parser : IParser<Map>
{
    private readonly Regex regex = new Regex(@"[A-Z]+", RegexOptions.Compiled);

    public Map Parse(string inputFile)
    {
        var map = new Map();
        string[] lines = File.ReadAllLines(inputFile);
        map.Directions = lines[0];

        foreach (var line in lines.Skip(2))
        {
            var matches = regex.Matches(line);

            var node = new Node()
            {
                Left = matches[1].Value,
                Right = matches[2].Value
            };

            map.Nodes.Add(matches[0].Value, node);
        }

        return map;
    }
}
