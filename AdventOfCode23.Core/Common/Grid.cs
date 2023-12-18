using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode23.Core.Common;

public class Grid<T>
{
    public T[][] GridValues { get; set; }
    public int Width => GridValues[0].Length;
    public int Height => GridValues.Length;

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
        set
        {
            GridValues[y][x] = value;
        }
    }

    public T this[Position pos]
    {
        get
        {
            return GridValues[pos.Y][pos.X];
        }
        set
        {
            GridValues[pos.Y][pos.X] = value;
        }
    }

    public Grid(List<List<T>> values)
    {
        if(!values.Select(l => l.Count()).All(l => l == values[0].Count()))
        {
            throw new ArgumentException("Grids should be rectangular and not have jagged members.");
        }

        GridValues = values.Select(x => x.ToArray()).ToArray();
    }

    public Grid(T[][] values)
    {
        if (!values.Select(l => l.Count()).All(l => l == values[0].Count()))
        {
            throw new ArgumentException("Grids should be rectangular and not have jagged members.");
        }

        GridValues = values;
    }

    public bool Contains(Position pos)
    {
        return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
    }

    public HashSet<T> UniqueValues()
    {
        return GridValues.SelectMany(x => x).ToHashSet();
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
