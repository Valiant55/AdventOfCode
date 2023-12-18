using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day17;

public class GearCity
{
    Grid<int> CityBlocks { get; set; }
    public GearCity(List<List<int>> cityBlocks)
    {
        CityBlocks = new Grid<int>(cityBlocks);
    }

    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
        NONE
    }

    private static readonly Dictionary<Direction, List<Direction>> ValidDirections = new()
    {
        { Direction.NORTH, [Direction.EAST, Direction.WEST] },
        { Direction.SOUTH, [Direction.EAST, Direction.WEST] },
        { Direction.EAST,  [Direction.NORTH, Direction.SOUTH] },
        { Direction.WEST,  [Direction.NORTH, Direction.SOUTH] },
        { Direction.NONE,  [Direction.NORTH, Direction.SOUTH, Direction.EAST, Direction.WEST] },
    };

    public record Node(Position Position, int HeatLoss, Direction Direction);

    public long FindShortestPath()
    {
        Grid<HashSet<Direction>> visited = new Grid<HashSet<Direction>>(InitVistedGrid());
        Position goal = new Position(CityBlocks.Width - 1, CityBlocks.Height - 1);

        PriorityQueue<Node, double> queue = new PriorityQueue<Node, double>();
        HashSet<Node> queuedNodes = new HashSet<Node>();
        queue.Enqueue(new Node(new Position(0, 0), 0, Direction.NONE), 0);

        long currentMin = long.MaxValue;

        while (queue.Count > 0)
        {
            Node curr = queue.Dequeue();

            if (curr.Position == goal)
            {
                currentMin = Math.Min(currentMin, curr.HeatLoss);
            }

            FindConnectingNodes(curr)
                .Where(n => !visited[n.Position].Contains(n.Direction))
                .ToList()
                .ForEach(n =>
                    {
                        if(queuedNodes.Add(n)) queue.Enqueue(n, n.HeatLoss + Potential(n.Position, goal));
                    }
                );

            visited[curr.Position].Add(curr.Direction);
        }

        return currentMin;
    }

    private List<Node> FindConnectingNodes(Node node)
    {
        List<Node> nodes = new List<Node>();

        foreach(var dir in ValidDirections[node.Direction])
        {
            var newPos = node.Position;
            var heat = node.HeatLoss;
            for(int i = 0; i < 3; i++)
            {
                newPos = Nudge(newPos, dir);
                if (CityBlocks.Contains(newPos))
                {
                    heat += CityBlocks[newPos];
                    Node newNode = new Node(newPos, heat, dir);
                    nodes.Add(newNode);
                }
                else
                {
                    break;
                }
            }
        }

        return nodes;
    }

    private HashSet<Direction>[][] InitVistedGrid()
    {
        HashSet<Direction>[][] visited = new HashSet<Direction>[CityBlocks.Height][];
        for(int y = 0; y < CityBlocks.Height; y++)
        {
            visited[y] = new HashSet<Direction>[CityBlocks.Width];
            for(int x = 0; x < CityBlocks.Width; x++)
            {
                visited[y][x] = new HashSet<Direction>();
            }
        }

        return visited;
    }

    private Position Nudge(Position pos, Direction dir)
    {
        switch (dir)
        {
            case Direction.NORTH:
                return new Position(pos.X, pos.Y - 1);
            case Direction.SOUTH:
                return new Position(pos.X, pos.Y + 1);
            case Direction.EAST:
                return new Position(pos.X + 1, pos.Y);
            default:
                return new Position(pos.X - 1, pos.Y);
        }
    }

    private double Potential(Position current, Position goal)
    {
        return Math.Sqrt(Math.Pow(current.X - goal.X, 2) + Math.Pow(current.Y - goal.Y, 2));
    }
}
