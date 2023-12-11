using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day01;

public class Parser : IParser<List<string>>
{
    public List<string> Parse(string inputFile)
    {
        return File.ReadAllLines(inputFile).ToList();
    }
}
