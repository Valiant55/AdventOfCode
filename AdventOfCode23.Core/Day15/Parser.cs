using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day15;

public class Parser : IParser<List<Step>>
{
    public List<Step> Parse(string inputFile)
    {
        var file = File.ReadAllText(inputFile);
        var steps = file.Split(",");

        return steps.Select(s => new Step(s)).ToList();
    }
}
