using System.Text;

namespace AdventOfCode23.Core.Day13;

public class LavaMap
{
    public char[][] Map { get; set; }

    public LavaMap(List<char[]> map)
    {
        Map = map.ToArray();
    }

    public long FindReflectionValues()
    {
        long count = 0;

        for(int i = 1; i < Map[0].Length; i++)
        {
            int len   = Math.Min(i, Map[0].Length - i);
            int start = i * 2 > Map[0].Length ? Map[0].Length - (len*2) : 0;
            if(Scan(start, len, GetColumn))
            {
                count += i;
            }
        }

        for (int i = 1; i < Map.Length; i++)
        {
            int len = Math.Min(i, Map.Length - i);
            int start = i * 2 > Map.Length ? Map.Length - (len * 2) : 0;
            if (Scan(start, len, GetRow))
            {
                count += i * 100;
            }
        }

        return count;
    }

    public bool Scan(int start, int length, Func<int, string> provider)
    {
        var left  = Enumerable.Range(start, length).Reverse();
        var right = Enumerable.Range(left.First() + 1, length);

        foreach((var l, var r ) in left.Zip(right))
        {
            var leftSide = provider(l);
            var rightSide = provider(r);
            if (leftSide != rightSide) return false;
        }

        return true;
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
