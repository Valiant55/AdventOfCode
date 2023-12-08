using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Utils;
using System.Reflection.Metadata;

namespace AdventOfCode23.Core.Day03;

public class EngineParser : IParser<IEnumerable<char[]>>
{
    public IEnumerable<char[]> Parse(string inputFile)
    {
        return File.ReadLines(inputFile).Select(l => l.ToArray());
    }
}
