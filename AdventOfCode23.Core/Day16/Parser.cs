using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day16;

public class Parser : IParser<MirrorArray>
{
    public MirrorArray Parse(string inputFile)
    {
        var lines = File.ReadAllLines(inputFile);
        return new MirrorArray(lines.Select(l => l.ToCharArray()).ToArray());
    }
}
