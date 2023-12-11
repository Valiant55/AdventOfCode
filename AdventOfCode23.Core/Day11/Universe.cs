using AdventOfCode23.Core.Common;
using AdventOfCode23.Core.Day10;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day11;

public class Universe
{
    public char[][] Observations { get; set; }

    public char[][] ExpandedObservations { get; set; }

    public List<Position> Galaxies { get; set; } = new List<Position>();

    public void ExpandUniverse()
    {
        List<List<char>> expansion = new();
        List<int> verticalExpansion = new();

        for (int row = 0; row < Observations.Length; row++)
        {
            expansion.Add(Observations[row].ToList());
            if(Observations[row].All(c => c == '.'))
            {
                expansion.Add(Observations[row].ToList());
            }
        }

        for(int col = 0; col < expansion[0].Count; col++)
        {
            List<char> newRow = new();
            bool allEmpty = true;
            foreach(var row in expansion)
            {
                if (row[col] == '#')
                {
                    allEmpty = false;
                    break;
                }
            }

            if(allEmpty)
            {
                verticalExpansion.Add(col);
            }
        }

        verticalExpansion.Reverse();
        foreach (var i in verticalExpansion)
        {
            foreach(var row in expansion)
            {
                row.Insert(i, '.');
            }
        }

        ExpandedObservations = expansion.Select(a => a.ToArray()).ToArray();
    }

    public void FindGalaxies()
    {
        for(int y = 0; y < ExpandedObservations.Length; y++)
        {
            for(int x = 0; x < ExpandedObservations[0].Length; x++)
            {
                if (ExpandedObservations[y][x] == '#') Galaxies.Add(new Position(x+1, y+1));
            }
        }
    }

    public long FindDistances()
    {
        ExpandUniverse();
        FindGalaxies();

        Console.WriteLine(Universe.Print(ExpandedObservations));

        long totalDistance = 0;

        foreach((var g1, var i) in Galaxies.Select((g, i) => (g, i+1)))
        {
            foreach(var g2 in Galaxies.Skip(i))
            {
                long distance = Math.Abs(g2.X - g1.X) + Math.Abs(g2.Y - g1.Y);
                totalDistance += distance;
            }
        }

        return totalDistance;
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
