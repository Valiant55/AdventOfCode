using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Utils;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day03;

public class PartNoParser : IParser<IEnumerable<PartNo>>
{
    private readonly Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

    public IEnumerable<PartNo> Parse(string inputFile)
    {
        List<PartNo> parts = new List<PartNo>();

        foreach (var line in File.ReadLines(inputFile).Select((l, i) => new { Value = l, Index = i }))
        {
            var matches = regex.Matches(line.Value);

            foreach (Match match in matches)
            {
                var id = int.Parse(match.Value);
                var lineNo = line.Index;
                var position = match.Index;
                var length = match.Length;

                var part = new PartNo(id, lineNo, position, length);
                parts.Add(part);
            }
        }

        return parts;
    }
}
