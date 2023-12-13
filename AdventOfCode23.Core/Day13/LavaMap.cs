using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode23.Core.Day13;

public class LavaMap
{
    public char[][] Map { get; set; }

    public LavaMap(List<char[]> map)
    {
        Map = map.ToArray();
    }

    public long FindReflectionValues(int differnces = 0)
    {
        long count = 0;

        count += FindVerticalDifferences()
            .Select((x, i) => (x, i))
            .Where(t => t.x == differnces)
            .Select(t => t.i + 1)
            .Sum();

        count += FindHorizontalDifferences()
            .Select((x, i) => (x, i))
            .Where(t => t.x == differnces)
            .Select(t => (t.i + 1) * 100)
            .Sum();

        return count;
    }

    public List<int> FindVerticalDifferences()
    {
        List<int> result = new();

        for (int i = 1; i < Map[0].Length; i++)
        {
            int len = Math.Min(i, Map[0].Length - i);
            int start = i * 2 > Map[0].Length ? Map[0].Length - (len * 2) : 0;

            result.Add(Scan(start, len, GetColumn));
        }

        return result;
    }

    public List<int> FindHorizontalDifferences()
    {
        List<int> result = new();

        for (int i = 1; i < Map.Length; i++)
        {
            int len = Math.Min(i, Map.Length - i);
            int start = i * 2 > Map.Length ? Map.Length - (len * 2) : 0;
            
            result.Add(Scan(start, len, GetRow));
        }

        return result;
    }

    public int Scan(int start, int length, Func<int, string> provider)
    {
        var left  = Enumerable.Range(start, length).Reverse();
        var right = Enumerable.Range(left.First() + 1, length);
        int differences = 0;

        foreach((var l, var r ) in left.Zip(right))
        {
            var leftSide = provider(l);
            var rightSide = provider(r);
            foreach((var c1, var c2) in leftSide.Zip(rightSide))
            {
                if(c1 != c2) differences++;
            }
        }

        return differences;
    }

    private string GetRow(int row)
    {
        return string.Join("", Map[row]);
    }

    private string GetColumn(int col)
    {
        var sb = new StringBuilder();

        foreach(var row in Map)
        {
            sb.Append(row[col]);
        }

        return sb.ToString();
    }
}
