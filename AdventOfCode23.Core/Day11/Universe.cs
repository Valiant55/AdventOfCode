using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day10;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day11;

public class Universe
{
    public char[][] Observations { get; set; }
    public List<Position> Galaxies { get; private set; } = new();
    public List<int> HorizontalExpansions { get; private set; } = new();
    public List<int> VerticalExpansions { get; private set; } = new();

    public Universe(char[][] observations)
    {
        Observations = observations;
        FindExpansions();
        FindGalaxies();
    }

    public void FindExpansions()
    {
        for (int row = 0; row < Observations.Length; row++)
        {
            if (Observations[row].All(c => c == '.'))
            {
                HorizontalExpansions.Add(row);
            }
        }

        for (int col = 0; col < Observations[0].Length; col++)
        {
            List<char> newRow = new();
            bool allEmpty = true;
            foreach(var row in Observations)
            {
                if (row[col] == '#')
                {
                    allEmpty = false;
                    break;
                }
            }

            if(allEmpty)
            {
                VerticalExpansions.Add(col);
            }
        }

    }

    public void FindGalaxies()
    {
        for(int y = 0; y < Observations.Length; y++)
        {
            for(int x = 0; x < Observations[0].Length; x++)
            {
                if (Observations[y][x] == '#') Galaxies.Add(new Position(x, y));
            }
        }
    }

    public long FindDistances(long fillFactor = 1)
    { 
        List<long> totalDistance = new();

        foreach((var g1, var i) in Galaxies.Select((g, i) => (g, i+1)))
        {
            foreach(var g2 in Galaxies.Skip(i))
            {
                long minX = Math.Min(g2.X, g1.X);
                long maxX = Math.Max(g2.X, g1.X);
                long minY = Math.Min(g2.Y, g1.Y);
                long maxY = Math.Max(g2.Y, g1.Y);

                long vert = VerticalExpansions
                    .Where(p => p > minX && p < maxX)
                    .Count();

                long horiz = HorizontalExpansions
                    .Where(p => p > minY && p < maxY)
                    .Count();

                long distance = (maxX - minX + (vert * fillFactor)) + (maxY - minY + (horiz * fillFactor));

                totalDistance.Add(distance);
            }
        }

        return totalDistance.Sum();
    }

    public static string Print<T>(T[][] array)
    {
        var sb = new StringBuilder();

        for (int row = 0; row < array.Length; row++)
        {
            for (int column = 0; column < array[0].Length; column++)
            {
                sb.Append(array[row][column].ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
