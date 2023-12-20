using AdventOfCode23.Core.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day19;

public class Parser : IParser<PartSorter>
{
    private readonly Regex rulesId = new Regex(@"^[a-z]+", RegexOptions.Compiled);
    private readonly Regex rulesString = new Regex(@"\{.+\}", RegexOptions.Compiled);
    private readonly Regex rulesRule = new Regex(@"([a-z])+([<|>])(\d+)\:([a-zA-Z]+)", RegexOptions.Compiled);

    private readonly Regex partsRegex = new Regex(@"(\d)+", RegexOptions.Compiled);

    public PartSorter Parse(string inputFile)
    {
        Dictionary<string, RuleSet> rulesMap = new();
        List<Part> partsList = new();

        string file = File.ReadAllText(inputFile);
        string[] split = file.Split("\r\n\r\n");

        foreach(string line in split[0].Split("\n"))
        {
            var id = rulesId.Match(line).Value;
            var ruleStrings = rulesString.Match(line).Value.Trim('{').Trim('}').Split(',');
            var finalDestination = ruleStrings.Last();
            List<Rule> rules = new List<Rule>();

            foreach (string r in ruleStrings.SkipLast(1))
            {
                var ruleMatches = rulesRule.Match(r);
                char category = ruleMatches.Groups[1].Value[0];
                char op = ruleMatches.Groups[2].Value[0];
                int value = int.Parse(ruleMatches.Groups[3].Value);
                string destination = ruleMatches.Groups[4].Value;

                rules.Add(new Rule() { Category = category, Operator = op, Value = value, Destination = destination });
            }

            rulesMap.Add(id, new RuleSet() { Rules = rules, FinalDestination = finalDestination});
        }

        foreach(string line in split[1].Split("\n"))
        {
            var matches = partsRegex.Matches(line);

            int x = int.Parse(matches[0].Value);
            int m = int.Parse(matches[1].Value);
            int a = int.Parse(matches[2].Value);
            int s = int.Parse(matches[3].Value);

            var part = new Part(x, m, a, s);
            partsList.Add(part);
        }

        return new PartSorter(rulesMap, partsList);
    }
}
