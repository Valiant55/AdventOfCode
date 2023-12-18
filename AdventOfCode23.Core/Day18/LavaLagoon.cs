using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day18;

public class DigInstruction
{
    public char Direction { get; set; }
    public int Distance { get; set; }
    public string Color { get; set; }
    public long TrueDistance { get; set; }
    public char TrueDirection { get; set; }

    public DigInstruction(char direction, int distance, string color)
    {
        Direction = direction;
        Distance = distance;
        Color = color;

        TrueDirection = Color[0] switch
        {
            '0' => 'R',
            '1' => 'D',
            '2' => 'L',
            '3' => 'U',
             _  => 'F'
        };

        TrueDistance = Convert.ToInt64(color[..5], 16);
    }
}

public class LavaLagoon
{
    public List<DigInstruction> DigPlan { get; set; }
    public Grid<char> Lagoon { get; set; }

    public LavaLagoon(List<DigInstruction> digPlan)
    {
        DigPlan = digPlan;

        int originXOffset = 0;
        int originYOffset = 0;

        int minXOffset = 0;
        int minYOffset = 0;
        int maxXOffset = 0;
        int maxYOffset = 0;

        foreach (var i in DigPlan)
        {
            switch (i.Direction)
            {
                case 'U':
                    originYOffset -= i.Distance;
                    minYOffset = Math.Min(minYOffset, originYOffset);
                    maxYOffset = Math.Max(maxYOffset, originYOffset);
                    break;
                case 'D':
                    originYOffset += i.Distance;
                    minYOffset = Math.Min(minYOffset, originYOffset);
                    maxYOffset = Math.Max(maxYOffset, originYOffset);
                    break;
                case 'L':
                    originXOffset -= i.Distance;
                    minXOffset = Math.Min(minXOffset, originXOffset);
                    maxXOffset = Math.Max(maxXOffset, originXOffset);
                    break;
                case 'R':
                    originXOffset += i.Distance;
                    minXOffset = Math.Min(minXOffset, originXOffset);
                    maxXOffset = Math.Max(maxXOffset, originXOffset);
                    break;
            }
        }

        Position origin = new Position(Math.Abs(minXOffset), Math.Abs(minYOffset));
        int width = Math.Abs(minXOffset) + Math.Abs(maxXOffset) + 1;
        int height = Math.Abs(minYOffset) + Math.Abs(maxYOffset) + 1;

        char[][] lagoon = new char[height][];
        for (int y = 0; y < height; y++)
        {
            lagoon[y] = Enumerable.Repeat('.', width).ToArray();
        }

        Lagoon = new Grid<char>(lagoon);
        Position current = origin;

        foreach (var i in DigPlan)
        {
            Lagoon[current] = '#';
            for(int m = 0; m < i.Distance; m++)
            {
                switch (i.Direction)
                {
                    case 'U':
                        current = current.MoveNorth();
                        break;
                    case 'D':
                        current = current.MoveSouth();
                        break;
                    case 'L':
                        current = current.MoveWest();
                        break;
                    case 'R':
                        current = current.MoveEast();
                        break;
                }

                Lagoon[current] = '#';
            }
        }

        FillPit();
    }

    public long CountPitArea()
    {
        long area = 0;

        for(int x = 0; x < Lagoon.Width; x++)
        {
            for(int y = 0; y < Lagoon.Height; y++)
            {
                if (Lagoon[y][x] == '#') area++;
            }
        }

        return area;
    }

    private void BuildPit()
    {

    }

    private void FillPit()
    {
        Queue<Position> pit = new Queue<Position>();
        HashSet<Position> set = new HashSet<Position>();

        Position firstContained = FindFirstInclosed();
        pit.Enqueue(firstContained);
        set.Add(firstContained);

        while (pit.TryDequeue(out var pos))
        {
            Lagoon[pos] = '#';

            if (Lagoon.Contains(pos.MoveNorth()) && Lagoon[pos.MoveNorth()] == '.' && set.Add(pos.MoveNorth())) pit.Enqueue(pos.MoveNorth());
            if (Lagoon.Contains(pos.MoveSouth()) && Lagoon[pos.MoveSouth()] == '.' && set.Add(pos.MoveSouth())) pit.Enqueue(pos.MoveSouth());
            if (Lagoon.Contains(pos.MoveEast())  && Lagoon[pos.MoveEast()]  == '.' && set.Add(pos.MoveEast())) pit.Enqueue(pos.MoveEast());
            if (Lagoon.Contains(pos.MoveWest())  && Lagoon[pos.MoveWest()]  == '.' && set.Add(pos.MoveWest())) pit.Enqueue(pos.MoveWest());
        }
    }

    private Position FindFirstInclosed()
    {
        for (int x = 0; x < Lagoon.Width; x++)
        {
            for (int y = 0; y < Lagoon.Height; y++)
            {
                if (Lagoon[y][x] == '.' && IsInclosed(x, y))
                {
                    return new Position(x, y);
                }
            }
        }

        return new Position(0, 0);
    }

    private bool IsInclosed(int row, int col)
    {
        int count = 0;

        for (int x = 0; x < row; x++)
        {
            if (Lagoon[col][x] == '#') count++;
        }

        return count % 2 == 1;
    }
}
