namespace AdventOfCode23.Core.Common;

public record Position(int X, int Y)
{
    public Position MoveNorth(int magnitude = 1)
    {
        return Move(new (0, -1), magnitude);
    }

    public Position MoveSouth(int magnitude = 1)
    {
        return Move(new(0, 1), magnitude);
    }

    public Position MoveEast(int magnitude = 1)
    {
        return Move(new(1, 0), magnitude);
    }

    public Position MoveWest(int magnitude = 1)
    {
        return Move(new(-1, 0), magnitude);
    }

    private Position Move(Tuple<int,int> tuple, int mag)
    {
        return new Position(X + (tuple.Item1 * mag), Y + (tuple.Item2 * mag));
    }
}