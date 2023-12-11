using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode23.Core.Common;

public class Grid<T> where T: class
{
    public T[][] GridValues { get; private set; }

    public T[] this[int index]
    {
        get
        {
            return GridValues[index];
        }
    }

    public T this[int y, int x]
    {
        get
        {
            return GridValues[y][x];
        }
    }

    public Grid(List<List<T>> values)
    {
        GridValues = values.Select(x => x.ToArray()).ToArray();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var r in GridValues)
        {
            foreach(var c in r)
            {
                sb.Append(c.ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
