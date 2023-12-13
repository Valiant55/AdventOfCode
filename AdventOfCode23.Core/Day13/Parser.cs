using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day13;

public class Parser : IParser<List<LavaMap>>
{
    public List<LavaMap> Parse(string inputFile)
    {
        var result = new List<LavaMap>();
        var file = File.ReadAllText(inputFile);
        var maps = file.Split("\r\n\r\n");

        foreach(var map in maps )
        {
            List<char[]> lavaMap = new List<char[]>();
            foreach(var line in map.Split("\r\n"))
            {
                lavaMap.Add(line.ToCharArray());
            }
            result.Add(new LavaMap(lavaMap));
        }

        return result;
    }
}
