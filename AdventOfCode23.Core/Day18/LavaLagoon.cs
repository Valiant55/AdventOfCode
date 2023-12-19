using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day18;

public class DigInstruction
{
    public char Direction { get; set; }
    public int Distance { get; set; }
    public string Color { get; set; }
    public int TrueDistance { get; set; }
    public char TrueDirection { get; set; }

    public DigInstruction(char direction, int distance, string color)
    {
        Direction = direction;
        Distance = distance;
        Color = color;
        TrueDistance = distance;
        TrueDirection = direction;

        TrueDirection = Color[5] switch
        {
            '0' => 'R',
            '1' => 'D',
            '2' => 'L',
            '3' => 'U',
             _  => 'F'
        };

        TrueDistance = Convert.ToInt32(color[..5], 16);
    }
}

public class LavaLagoon
{
    public List<DigInstruction> DigPlan { get; set; }

    public LavaLagoon(List<DigInstruction> digPlan)
    {
        DigPlan = digPlan;
    }

    public long CountPitArea()
    {
        Grid<char> lagoon = BuildPit(DigPlan);
        FillPit(lagoon);

        long area = 0;

        for (int x = 0; x < lagoon.Width; x++)
        {
            for (int y = 0; y < lagoon.Height; y++)
            {
                if (lagoon[y][x] == '#') area++;
            }
        }

        return area;
    }

    public long CountCorrectedPitArea()
    {
        (long X, long Y) currentLocation = new(0, 0);
        List<(long X, long Y)> verticies = [currentLocation];
        long boarder = 0;

        foreach (var i in DigPlan)
        {
            switch (i.TrueDirection)
            {
                case 'U':
                    currentLocation.Y -= i.TrueDistance;
                    break;
                case 'D':
                    currentLocation.Y += i.TrueDistance;
                    break;
                case 'L':
                    currentLocation.X -= i.TrueDistance;
                    break;
                case 'R':
                    currentLocation.X += i.TrueDistance;
                    break;
            }
            boarder += i.TrueDistance;
            verticies.Add(currentLocation);
        }
        verticies = verticies.SkipLast(1).ToList();

        long area = 0;
        currentLocation = verticies[0];
        foreach (var vertex in verticies.Skip(1))
        {
            area += (currentLocation.X * vertex.Y) - (currentLocation.Y * vertex.X);
            currentLocation = vertex;
        }

        area += (verticies[0].X * verticies.Last().Y) - (verticies[0].Y * verticies.Last().X);
        area = Math.Abs(area);

        return ((area + boarder) / 2) + 1;
    }

    private Grid<char> BuildPit(List<DigInstruction> digPlan)
    {
        int originXOffset = 0;
        int originYOffset = 0;

        int minXOffset = 0;
        int minYOffset = 0;
        int maxXOffset = 0;
        int maxYOffset = 0;

        foreach (var i in digPlan)
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

        Grid<char> gridLagoon = new Grid<char>(lagoon);
        Position current = origin;

        foreach (var i in digPlan)
        {
            gridLagoon[current] = '#';
            for (int m = 0; m < i.Distance; m++)
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

                gridLagoon[current] = '#';
            }
        }

        return gridLagoon;
    }

    private void FillPit(Grid<char> lagoon)
    {
        Queue<Position> pit = new Queue<Position>();
        HashSet<Position> set = new HashSet<Position>();

        Position firstContained = FindFirstInclosed(lagoon);
        pit.Enqueue(firstContained);
        set.Add(firstContained);

        while (pit.TryDequeue(out var pos))
        {
            lagoon[pos] = '#';

            if (lagoon.Contains(pos.MoveNorth()) && lagoon[pos.MoveNorth()] == '.' && set.Add(pos.MoveNorth())) pit.Enqueue(pos.MoveNorth());
            if (lagoon.Contains(pos.MoveSouth()) && lagoon[pos.MoveSouth()] == '.' && set.Add(pos.MoveSouth())) pit.Enqueue(pos.MoveSouth());
            if (lagoon.Contains(pos.MoveEast())  && lagoon[pos.MoveEast()]  == '.' && set.Add(pos.MoveEast())) pit.Enqueue(pos.MoveEast());
            if (lagoon.Contains(pos.MoveWest())  && lagoon[pos.MoveWest()]  == '.' && set.Add(pos.MoveWest())) pit.Enqueue(pos.MoveWest());
        }
    }

    private Position FindFirstInclosed(Grid<char> lagoon)
    {
        for (int x = 0; x < lagoon.Width; x++)
        {
            for (int y = 0; y < lagoon.Height; y++)
            {
                if (lagoon[y][x] == '.' && IsInclosed(lagoon, x, y))
                {
                    return new Position(x, y);
                }
            }
        }

        return new Position(0, 0);
    }

    private bool IsInclosed(Grid<char> lagoon, int row, int col)
    {
        int count = 0;

        for (int x = 0; x < row; x++)
        {
            if (lagoon[col][x] == '#') count++;
        }

        return count % 2 == 1;
    }
}
