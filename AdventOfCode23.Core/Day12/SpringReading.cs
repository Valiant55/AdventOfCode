namespace AdventOfCode23.Core.Day12;

public class SpringReading
{
    public string Reading { get; set; }
    public List<int> DamagedGroups { get; set; }
    public Dictionary<string, long> Cache {  get; set; }

    public SpringReading(string reading, List<int> groups)
    {
        Reading = reading;
        DamagedGroups = groups.Where(i => i > 0).ToList();
        Cache = new Dictionary<string, long>();
    }

    public long CountArrangements()
    {
        return CountArrangements(Reading, DamagedGroups);
    }

    private long CountArrangements(string reading, List<int> groups)
    {
        if (string.IsNullOrEmpty(reading))
        {
            return groups.Count == 0 ? 1 : 0;
        }

        if(groups.Count == 0)
        {
            return reading.Contains("#") ? 0 : 1;
        }

        var key = $"{reading}|{string.Join(",",groups)}";
        if(Cache.ContainsKey(key))
        {
            return Cache[key];
        }

        long count = 0;

        if(reading.StartsWith(".") || reading.StartsWith("?"))
        {
            count += CountArrangements(reading[1..], groups);
        }

        if (reading.StartsWith("#") || reading.StartsWith("?"))
        {
            if (groups[0] <= reading.Length && 
                !reading[..groups[0]].Contains(".") && 
                (groups[0] == reading.Length || reading[groups[0]] != '#')
            )
            {
                var slice = groups[0] + 1 <= reading.Length ? reading[(groups[0] + 1)..] : "";
                count += CountArrangements(slice, groups.Skip(1).ToList());
            }
        }

        Cache.Add(key, count);

        return count;
    }
}
