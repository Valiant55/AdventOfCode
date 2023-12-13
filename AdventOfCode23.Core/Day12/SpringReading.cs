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

}
