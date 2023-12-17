using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day17;

public class Parser : IParser<GearCity>
{
    public GearCity Parse(string inputFile)
    {
        var result = File.ReadAllLines(inputFile);
        List<List<int>> grid = result
            .Select(l => l
                .Select(c => int.Parse(c.ToString()))
                .ToList()
            )
            .ToList();

        return new GearCity(grid);
    }
}
