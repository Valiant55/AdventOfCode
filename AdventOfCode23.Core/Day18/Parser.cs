using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day18;

public class Parser : IParser<LavaLagoon>
{
    private static readonly Regex regex = new Regex(@"[a-zA-Z0-9]+", RegexOptions.Compiled);

    public LavaLagoon Parse(string inputFile)
    {
        List<DigInstruction> digPlan = new List<DigInstruction>();

        foreach(var line in File.ReadLines(inputFile))
        {
            var matches = regex.Matches(line);
            var direction = matches[0].Value[0];
            var distance = int.Parse(matches[1].Value);
            var color = matches[2].Value;

            digPlan.Add(new DigInstruction(direction, distance, color));
        }

        return new LavaLagoon(digPlan);
    }
}
