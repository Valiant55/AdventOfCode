using AdventOfCode23.Core.Common;
using System.Linq;
using System.Text;

namespace AdventOfCode23.Core.Day10;

public class PipeMap
{
    public Pipe[,] Pipes { get; set; }
    public char[,] PipeMask { get; set; }
    public Position StartingPosition { get; set; }
    private char[] Intersections => new char[] { '|', 'L', 'J' }; 

    public long FindMaxDistance()
    {
        long totalDistance = 0;
        Position currentPosition = StartingPosition;
        Position lastPostion = StartingPosition;

        do
        {
            var currentPipe = Pipes[currentPosition.Y, currentPosition.X];

            foreach ((var neighbor, var direction) in FindNeighbors(currentPosition).Where(p => p.Item1 != lastPostion))
            {
                var neighborPipe = Pipes[neighbor.Y , neighbor.X];

                if (currentPipe.ConnectsTo(neighborPipe, direction))
                {
                    lastPostion = currentPosition;
                    currentPosition = neighbor;
                    totalDistance++;
                    break;
                }
            }
        }
        while (currentPosition != StartingPosition);

        return totalDistance / 2;
    }

    public List<(Position, Direction)> FindNeighbors(Position currentLocation)
    {
        List<(Position, Direction)> result = new List<(Position, Direction)>();

        if (currentLocation.X - 1 >= 0) result.Add((new Position(currentLocation.X - 1, currentLocation.Y), Direction.WEST));
        if (currentLocation.X + 1 < Pipes.GetLength(1)) result.Add((new Position(currentLocation.X + 1, currentLocation.Y), Direction.EAST));
        if (currentLocation.Y - 1 >= 0) result.Add((new Position(currentLocation.X, currentLocation.Y - 1), Direction.NORTH));
        if (currentLocation.Y + 1 < Pipes.GetLength(0)) result.Add((new Position(currentLocation.X, currentLocation.Y + 1), Direction.SOUTH));

        return result;
    }

    public long FindInclosedArea()
    {
        InitPipeMask();
        Position currentPosition = StartingPosition;
        Position lastPostion = StartingPosition;

        do
        {
            var currentPipe = Pipes[currentPosition.Y, currentPosition.X];

            foreach ((var neighbor, var direction) in FindNeighbors(currentPosition).Where(p => p.Item1 != lastPostion))
            {
                var neighborPipe = Pipes[neighbor.Y, neighbor.X];

                if (currentPipe.ConnectsTo(neighborPipe, direction))
                {
                    lastPostion = currentPosition;
                    currentPosition = neighbor;
                    /* Connected Pipe */
                    PipeMask[lastPostion.Y, lastPostion.X] = 'X';
                    break;
                }
            }
        }
        while (currentPosition != StartingPosition);

        for (int row = 0; row < PipeMask.GetLength(0); row++)
        {
            for (int column = 0; column < PipeMask.GetLength(1); column++)
            {
                if (PipeMask[row, column] == 'U')
                {
                    if (IsInclosed(row, column))
                    {
                        PipeMask[row, column] = 'I';
                    }
                    else
                    {
                        PipeMask[row, column] = 'O';
                    }
                }
            }
        }

        long result = 0;
        foreach(char c in PipeMask)
        {
            if (c == 'I') result++;
        }

        return result;
    }

    private bool IsInclosed(int row, int column)
    {
        int counts = 0;

        for (int x = 0; x < column; x++)
        {
            if (PipeMask[row, x] == 'X' && Intersections.Contains(Pipes[row, x].PipeGlyph)) counts++;
        }

        return counts % 2 == 1;
    }

    private void InitPipeMask()
    {
        PipeMask = new char[Pipes.GetLength(0), Pipes.GetLength(1)];

        for (int row = 0; row < PipeMask.GetLength(0); row++)
        {
            for (int column = 0; column < PipeMask.GetLength(1); column++)
            {

                if (row == 0 || column == 0 || row == PipeMask.GetLength(0) - 1 || column == PipeMask.GetLength(1) - 1)
                {
                    /* Not Inclosed */
                    PipeMask[row, column] = 'O';
                }
                else
                {
                    /* Unknown */
                    PipeMask[row, column] = 'U';
                }
            }
        }
    }

    public static string Print<T>(T[,] array)
    {
        var sb = new StringBuilder();

        for (int row = 0; row < array.GetLength(0); row++)
        {
            for (int column = 0; column < array.GetLength(1); column++)
            {
                sb.Append(array[row, column].ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        return PipeMap.Print(Pipes);
    }
}
