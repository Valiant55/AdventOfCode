namespace AdventOfCode23.Core.Day10;

public enum Direction
{
    EAST,
    WEST,
    NORTH,
    SOUTH
}

public class Pipe
{
    public char PipeGlyph { get; set; }
    public List<Direction> Connections { get; set; }

    public static Dictionary<char, List<Direction>> pipeMap = new()
    {
        { '|', [Direction.NORTH, Direction.SOUTH] },
        { '-', [Direction.WEST,  Direction.EAST]  },
        { 'L', [Direction.NORTH, Direction.EAST]  },
        { 'J', [Direction.NORTH, Direction.WEST]  },
        { '7', [Direction.SOUTH, Direction.WEST]  },
        { 'F', [Direction.SOUTH, Direction.EAST]  },
        { '.', [] },
        { 'S', [Direction.NORTH, Direction.SOUTH, Direction.WEST,  Direction.EAST] },
    };

    public static Dictionary<Direction, Direction> ConnectionMap = new()
    {
        { Direction.SOUTH, Direction.NORTH },
        { Direction.NORTH, Direction.SOUTH },
        { Direction.WEST,  Direction.EAST  },
        { Direction.EAST,  Direction.WEST  }

    };

    public Pipe(char pipeGlyph)
    {
        PipeGlyph = pipeGlyph;
        Connections = pipeMap[pipeGlyph];
    }

    public bool ConnectsTo(Pipe other, Direction direction)
    {
        if (!this.Connections.Contains(direction)) return false;
        if (!other.Connections.Contains(ConnectionMap[direction])) return false;
        return true;
    }

    public override string ToString()
    {
        return $"{PipeGlyph}";
    }
}
