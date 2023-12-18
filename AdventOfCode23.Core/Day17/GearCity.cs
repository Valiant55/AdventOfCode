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
        Grid<bool> visited = new Grid<bool>(InitVistedGrid());
        Grid<int> shortestPath = new Grid<int>(InitShortestPath());
        Grid<List<Direction>> incomingDireciton = new Grid<List<Direction>>(InitIncomingDirection());

        while (TryFindMinimum(visited, shortestPath, out var currPosition))
        {
            List<Node> unvistedNeighbors = new();

            foreach(var dir in incomingDireciton[currPosition])
            {
                var curr = new Node(currPosition, shortestPath[currPosition], dir);
                unvistedNeighbors = unvistedNeighbors.Concat(FindConnectingNodes(curr)).ToList();
            }

            unvistedNeighbors = unvistedNeighbors
                .Where(n => !visited[n.Position])
                .ToList();

            foreach(var node in unvistedNeighbors)
            {
                if(node.HeatLoss <= shortestPath[node.Position])
                {
                    shortestPath[node.Position] = node.HeatLoss;
                    incomingDireciton[node.Position].Add(node.Direction);
                }
            }

            visited[currPosition] = true;
        }

        Console.WriteLine(shortestPath);

        return shortestPath[CityBlocks.Height - 1][CityBlocks.Width - 1];
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

    private bool[][] InitVistedGrid()
    {
        bool[][] visited = new bool[CityBlocks.Height][];
        for(int y = 0; y < CityBlocks.Height; y++)
        {
            visited[y] = Enumerable.Repeat(false, CityBlocks.Width).ToArray();
        }

        return visited;
    }

    private int[][] InitShortestPath()
    {
        int[][] shortest = new int[CityBlocks.Height][];
        for (int y = 0; y < CityBlocks.Height; y++)
        {
            shortest[y] = Enumerable.Repeat(int.MaxValue, CityBlocks.Width).ToArray();
        }

        shortest[0][0] = 0;

        return shortest;
    }

    private List<Direction>[][] InitIncomingDirection()
    {
        List<Direction>[][] direcitons = new List<Direction>[CityBlocks.Height][];
        for (int y = 0; y < CityBlocks.Height; y++)
        {
            direcitons[y] = new List<Direction>[CityBlocks.Width];
            for(int x = 0; x < CityBlocks.Width; x++)
            {
                direcitons[y][x] = new List<Direction>();
            }
        }

        direcitons[0][0].Add(Direction.NONE);

        return direcitons;
    }

    private bool TryFindMinimum(Grid<bool> visited, Grid<int> shortestPath, out Position position)
    {
        Position pos = null;
        int currentMax = int.MaxValue;

        for (int y = 0; y < CityBlocks.Height; y++)
        {
            for (int x = 0; x < CityBlocks.Width; x++)
            {
                if (!visited[y][x] && shortestPath[y][x] < currentMax)
                {
                    pos = new Position(x, y);
                    currentMax = shortestPath[y][x];
                }
            }
        }

        position = pos;
        return pos is not null;
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
}
