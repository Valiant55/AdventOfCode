using AdventOfCode23.Core.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day12;
public enum Condition
{
    DAMAGED,
    OPERATIONAL,
    UNKNOWN
}

public class SpringReading
{
    public string Reading { get; set; }
    public List<Condition> SpringConditions { get; set; }
    public List<int> DamagedGroups { get; set; }
    public List<int> OperationalGroups { get; set; }
    public List<Condition> ConditionChoices { get; set; }

    public int TotalUnkown => SpringConditions.Where(r => r == Condition.UNKNOWN).Count();
    public int KnownDamagedSprings => SpringConditions.Where(r => r == Condition.DAMAGED).Count();
    public int TotalDamagedSprings => DamagedGroups.Sum();

    private static readonly Regex regex = new Regex(@"\.+", RegexOptions.Compiled);

    private static readonly Dictionary<char, Condition> conditionMap = new()
    {
        { '#', Condition.DAMAGED },
        { '.', Condition.OPERATIONAL },
        { '?', Condition.UNKNOWN },
    };

    private static readonly Dictionary<Condition, char> conditionCharMap = new()
    {
        { Condition.DAMAGED,     '#'},
        { Condition.OPERATIONAL, '.'},
        { Condition.UNKNOWN,     '?'},
    };

    public SpringReading(string reading, List<int> groups)
    {
        Reading = reading;
        DamagedGroups = groups.Where(i => i > 0).ToList();
        SpringConditions = new List<Condition>();
        ConditionChoices = new List<Condition>();

        foreach(var c in reading)
        {
            SpringConditions.Add(conditionMap[c]);
        }

        int remainingDamaged = TotalDamagedSprings - KnownDamagedSprings;
        int remainingOperational = TotalUnkown - remainingDamaged;

        ConditionChoices = Enumerable
            .Repeat(Condition.DAMAGED, remainingDamaged)
            .Concat(Enumerable.Repeat(Condition.OPERATIONAL, remainingOperational))
            .ToList();

        OperationalGroups = new();

        OperationalGroups.Add(0);
        OperationalGroups = OperationalGroups.Concat(Enumerable.Repeat(1, DamagedGroups.Count-1)).ToList();
        OperationalGroups.Add(0);
    }

    public long CountArrangements()
    {
        long count = 0;

        count += CountMatchesByOffset(new(OperationalGroups), 0);

        return count;
    }

    private long CountMatchesByOffset(List<int> opGroups, int offset)
    {
        long count = 0;
        List<Condition> pattern = BuildPattern(opGroups);

        while(opGroups.Sum() + DamagedGroups.Sum() <= SpringConditions.Count)
        {
            for (int i = offset + 1; i < OperationalGroups.Count; i++)
            {
                List<int> copy = new(opGroups);
                copy[i]++;
                count += CountMatchesByOffset(copy, i);
            }

            if (MatchesRequiredPattern(pattern))
            {
                count++;
            }

            opGroups[offset]++;
            pattern = BuildPattern(opGroups);
  
        }

        return count;
    }

    private List<Condition> BuildPattern(List<int> operational)
    {
        IEnumerable<Condition> conditions = new List<Condition>();

        conditions = conditions
                .Concat(Enumerable.Repeat(Condition.OPERATIONAL, operational.First()));

        foreach ((var d, var o) in DamagedGroups.Zip(operational.Skip(1)))
        {
            conditions = conditions
                .Concat(Enumerable.Repeat(Condition.DAMAGED, d))
                .Concat(Enumerable.Repeat(Condition.OPERATIONAL, o));
        }

        return conditions.ToList();
    }

    private bool MatchesRequiredPattern(List<Condition> required)
    {
        if (required.Count != SpringConditions.Count)
        {
            return false;
        }

        foreach((var req, var con) in required.Zip(SpringConditions))
        {
            if (con != Condition.UNKNOWN && req != con) return false;
        }

        return true;
    }

    private IEnumerable<List<Condition>> GenerateReplacements()
    {
        var permutations = ConditionChoices
            .Permutations()
            .ToList();

        foreach(var perm in permutations)
        {
            List<Condition> replacement = new(SpringConditions);
            List<int> unknownIndexs = SpringConditions
                .Select((c, i) => new { Condition = c, Index = i})
                .Where(a => a.Condition == Condition.UNKNOWN)
                .Select(a => a.Index)
                .ToList();

            foreach((var unknown, var index) in unknownIndexs.Select((u, i) => (u, i)))
            {
                replacement[unknown] = perm[index];
            }
            
            yield return replacement;
        }
    }

    private bool MatchesPattern(List<Condition> conditions)
    {
        List<List<Condition>> grouped = GroupDamaged(conditions);
        List<int> groupCounts = grouped.Select(l => l.Count).ToList();

        foreach ((var current, var expected) in DamagedGroups.Zip(groupCounts))
        {
            if (current != expected) return false;
        }

        return true;
    }

    private List<List<Condition>> GroupDamaged(List<Condition> conditions)
    {
        var result = new List<List<Condition>>();
        var group = new List<Condition>();

        foreach(var c in conditions)
        {
            if(c == Condition.DAMAGED)
            {
                group.Add(c);
            }
            else if(group.Count > 0)
            {
                result.Add(group);
                group = new List<Condition>();
            }
        }

        if (group.Count > 0)
        {
            result.Add(group);
        }

        return result;
    }

    private string Print(List<Condition> list)
    {
        var sb = new StringBuilder();

        foreach (var c in list)
        {
            sb.Append(conditionCharMap[c]);
        }

        return sb.ToString();
    }
}

class ConditionListComparer : IEqualityComparer<List<Condition>>
{
    public bool Equals(List<Condition>? x, List<Condition>? y)
    {
        if (Object.ReferenceEquals(x, y)) return true;

        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            return false;

        foreach((var c1, var c2) in x.Zip(y))
        {
            if (c1 != c2) return false;
        }

        return true;
    }

    public int GetHashCode([DisallowNull] List<Condition> obj)
    {
        if (Object.ReferenceEquals(obj, null)) return 0;

        return obj.Select(c => c.GetHashCode()).Aggregate((a, i) => a ^ i);
    }
}