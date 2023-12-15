using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day14;

public class Parser : IParser<Platform>
{
    public Platform Parse(string inputFile)
    {
        List<char[]> map = new();

        foreach(var line in File.ReadLines(inputFile))
        {
            map.Add(line.ToCharArray());
        }

        return new Platform() { Map = map.ToArray()};
    }
}
