using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day09;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
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

    private static readonly Regex regex = new Regex(@"\#+", RegexOptions.Compiled);

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

    public static Dictionary<string, long> Cache = new()
    {

    };

    public static long CacheHit = 0;
    public static long CacheMiss = 0;

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

    public long CountArrangementsCached()
    {
        string key = $"{Reading.Length} {string.Join(",", DamagedGroups)}";

        for(int i = 1; i <= Reading.Length; i++)
        {
            string input = Reading.Substring(0, i);
            List<string> strings = GenerateStrings(input);
            List<(string, int)> patterns = strings
                .Select(s => ReadingToDamagedGroups(s))
                .Select(s => string.Join(",", s))
                .GroupBy(s => s)
                .Select(g => (g.Key, g.Count()))
                .ToList();

            foreach(var pattern in patterns)
            {
                Cache.TryAdd($"{input} | {pattern.Item1}", pattern.Item2);
            }
        }

        return Cache.GetValueOrDefault(key);
    }

    private List<string> GenerateStrings(string reading)
    {
        if(reading == "?") return new List<string>() { "#", "." };
        if(reading.Length == 1) return new List<string>() { reading };

        IEnumerable<string> result = new List<string>();
        var markLoc = reading.IndexOf("?");

        if(markLoc != -1)
        {
            result = result.Concat(GenerateStrings(reading.Remove(markLoc, 1).Insert(markLoc, "#")));
            result = result.Concat(GenerateStrings(reading.Remove(markLoc, 1).Insert(markLoc, ".")));
        }
        else
        {
            result = result.Append(reading);
        }

        return result.ToList();
    }

    private long CountArrangementsCached(string reading)
    {
        if (reading == "#" || reading == "?") return 1;
        if (reading == "." || reading == string.Empty) return 0;

        string key = $"{reading.Length} {string.Join(",", ReadingToDamagedGroups(reading))}";

        if (Cache.TryGetValue(key, out var count))
        {
            CacheHit++;
            return count;
        }
        CacheMiss++;

        string chop = reading.Substring(0, reading.Length - 1);
        if (reading.EndsWith("?"))
        {
            count += CountArrangementsCached($"{chop}#");
            count += CountArrangementsCached($"{chop}.");
        }
        else
        {
            count += CountArrangementsCached($"{chop}");
        }

        Cache.TryAdd(key, count);

        return count;
    }

    private List<int> ReadingToDamagedGroups(string reading)
    {
        var matches = regex.Matches(reading);
        
        return matches.Select(m => m.Value.Length).ToList();
    }
}
