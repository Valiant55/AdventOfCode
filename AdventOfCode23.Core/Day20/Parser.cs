using AdventOfCode23.Core.Common;
using System.Collections.Generic;

namespace AdventOfCode23.Core.Day20;

public class Parser : IParser<Machines>
{
    public Machines Parse(string inputFile)
    {
        List<(string, List<string>)> result = new();

        foreach (var line in File.ReadLines(inputFile))
        {
            var lineArray = line.Split("->");
            string id = lineArray[0].Trim();

            var connectedNodes = lineArray[1]
                .Split(",")
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();

            result.Add((id, connectedNodes));
        }

        return new Machines(result);
    }
}
