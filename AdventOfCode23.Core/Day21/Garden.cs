using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day21;

public class Garden
{
    Grid<char> Map { get; set; }
    Position Start { get; set; }

    public Garden(List<string> map)
    {
        Map = new Grid<char>(map.Select(s => s.ToCharArray()).ToArray());
        Start = new Position(0, 0);

        for(int y = 0; y < Map.Height; y++)
        {
            if (Map[y].Contains('S'))
            {
                int x = Array.IndexOf(Map[y], 'S');
                Start = new Position(x, y);
            }
        }
    }

    public long CountPlots(int steps = 64)
    {
        long countSteps = 0;
        Queue<Position> positions = new();
        HashSet<Position> nextPositions = new();
        positions.Enqueue(Start);

        while(countSteps < steps)
        {
            nextPositions.Clear();
            while (positions.TryDequeue(out var current))
            {
                if (Map.Contains(current.MoveNorth()) && Map[current.MoveNorth()] != '#') nextPositions.Add(current.MoveNorth());
                if (Map.Contains(current.MoveSouth()) && Map[current.MoveSouth()] != '#') nextPositions.Add(current.MoveSouth());
                if (Map.Contains(current.MoveWest()) && Map[current.MoveWest()]   != '#') nextPositions.Add(current.MoveWest());
                if (Map.Contains(current.MoveEast()) && Map[current.MoveEast()]   != '#') nextPositions.Add(current.MoveEast());
            }
            countSteps++;
            positions = new Queue<Position>(nextPositions);
        }

        return nextPositions.Count();
    }
}
