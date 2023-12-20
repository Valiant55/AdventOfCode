namespace AdventOfCode23.Core.Day19;

public record Part
{
    public long X { get; set; }
    public long M { get; set; }
    public long A { get; set; }
    public long S { get; set; }

    public long Rating => X + M + A + S;

    public Part(long x, long m, long a, long s)
    {
        X = x;
        M = m;
        A = a;
        S = s;
    }

    public long GetByCategory(char category)
    {
        return category switch
        {
            'x' => X,
            'm' => M,
            'a' => A,
            's' => S,
            _ => 0
        };
    }

    public Part SetMin(char category, long value)
    {
        Part newPart = new(X, M, A, S);
        switch (category)
        {
            case 'x':
                newPart.X = Math.Max(newPart.X, value);
                break;
            case 'm':
                newPart.M = Math.Max(newPart.M, value);
                break;
            case 'a':
                newPart.A = Math.Max(newPart.A, value);
                break;
            case 's':
                newPart.S = Math.Max(newPart.S, value);
                break;
        }

        return newPart;
    }

    public Part SetMax(char category, long value)
    {
        Part newPart = new(X, M, A, S);
        switch (category)
        {
            case 'x':
                newPart.X = Math.Min(newPart.X, value);
                break;
            case 'm':
                newPart.M = Math.Min(newPart.M, value);
                break;
            case 'a':
                newPart.A = Math.Min(newPart.A, value);
                break;
            case 's':
                newPart.S = Math.Min(newPart.S, value);
                break;
        }

        return newPart;
    }

    public long Combinations(Part other)
    {
        return Math.Abs(X - other.X + 1) * Math.Abs(M - other.M + 1) * Math.Abs(A - other.A + 1) * Math.Abs(S - other.S + 1);
    }
}

public class RuleSet
{
    public List<Rule> Rules { get; set; }
    public string FinalDestination { get; set; } = string.Empty;

    public string Apply(Part part)
    {
        foreach (var rule in Rules)
        {
            if (rule.Passes(part)) return rule.Destination;
        }

        return FinalDestination;
    }

}

public class Rule()
{
    public char Category { get; set; }
    public long Value { get; set; }
    public char Operator { get; set; }
    public string Destination { get; set; } = string.Empty;

    public bool Passes(Part part)
    {
        return Operator switch
        {
            '>' => GreaterThan(part.GetByCategory(Category)),
            '<' => LessThan(part.GetByCategory(Category)),
            _ => false
        };
    }

    private bool GreaterThan(long c)
    {
        return c > Value;
    }

    private bool LessThan(long c)
    {
        return c < Value;
    }

}

public class PartSorter
{
    public Dictionary<string, RuleSet> RulesMap { get; set; }
    public List<Part> Parts { get; set; }

    public PartSorter(Dictionary<string, RuleSet> rulesMap, List<Part> parts)
    {
        RulesMap = rulesMap;
        Parts = parts;
    }

    public long SortParts()
    {
        List<Part> accepted = new List<Part>();

        foreach (var part in Parts)
        {
            var currentDestination = "in";

            while(RulesMap.TryGetValue(currentDestination, out var ruleSet))
            {
                currentDestination = ruleSet.Apply(part);
            }

            if(currentDestination == "A") accepted.Add(part);
        }

        return accepted.Select(p => p.Rating).Sum();
    }

    public long FindCombinations()
    {
        long comobs = 0;
        Queue<(Part minPart, Part maxPart, string id)> queue = new Queue<(Part, Part, string)> ();
        List<(Part minPart, Part maxPart)> results = new();
        queue.Enqueue((new Part(1, 1, 1, 1), new Part(4000, 4000, 4000, 4000), "in"));

        while(queue.TryDequeue(out var item))
        {
            if (item.id == "R") continue;
            else if (item.id == "A")
            {
                results.Add((item.minPart, item.maxPart));
                comobs += item.maxPart.Combinations(item.minPart);
                continue;
            }

            var minPassThru = item.minPart;
            var maxPassThru = item.maxPart;
            var ruleSet = RulesMap[item.id];
            foreach(var rule in ruleSet.Rules)
            {
                switch (rule.Operator)
                {
                    case '>':
                        queue.Enqueue((minPassThru.SetMin(rule.Category, rule.Value+1), maxPassThru, rule.Destination));
                        maxPassThru = maxPassThru.SetMax(rule.Category, rule.Value);
                        break;
                    case '<':
                        queue.Enqueue((minPassThru, maxPassThru.SetMax(rule.Category, rule.Value-1), rule.Destination)); 
                        minPassThru = minPassThru.SetMin(rule.Category, rule.Value);
                        break;
                }
            }

            queue.Enqueue((minPassThru, maxPassThru, ruleSet.FinalDestination));
        }

        return comobs;
    }
}
