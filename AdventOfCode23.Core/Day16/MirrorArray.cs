namespace AdventOfCode23.Core.Day16;

public enum Direction
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}

public record Vector(int X, int Y, Direction Direction);


public class MirrorArray
{
    public char[][] Array {  get; set; }
    public HashSet<Vector> TraveledNodes { get; set; }

    private static Dictionary<(Direction, char), List<Direction>> ReflectionMap = new()
    {
        { (Direction.EAST,  '|'),  [Direction.NORTH, Direction.SOUTH]},
        { (Direction.WEST,  '|'),  [Direction.NORTH, Direction.SOUTH]},
        { (Direction.NORTH, '-'),  [Direction.EAST, Direction.WEST]},
        { (Direction.SOUTH, '-'),  [Direction.EAST, Direction.WEST]},
        { (Direction.NORTH, '\\'), [Direction.WEST]},
        { (Direction.SOUTH, '\\'), [Direction.EAST]},
        { (Direction.EAST,  '\\'), [Direction.SOUTH]},
        { (Direction.WEST,  '\\'), [Direction.NORTH]},
        { (Direction.NORTH, '/'),  [Direction.EAST]},
        { (Direction.SOUTH, '/'),  [Direction.WEST]},
        { (Direction.EAST,  '/'),  [Direction.NORTH]},
        { (Direction.WEST,  '/'),  [Direction.SOUTH]},
    };

    public MirrorArray(char[][] array)
    {
        Array = array;
        TraveledNodes = [];
    }

    public long CountTraveledNodes()
    {
        FireLaser(new Vector(0, 0, Direction.EAST));
        return TraveledNodes.Select(v => (v.X, v.Y)).Distinct().Count();
    }

    public void FireLaser(Vector startingVector)
    {
        var queue = new Queue<Vector>();
        queue.Enqueue(startingVector);

        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();
            if(TraveledNodes.Contains(curr))
            {
                continue;
            }
            TraveledNodes.Add(curr);

            var directions = ReflectionMap
                .GetValueOrDefault(
                    (curr.Direction, Array[curr.Y][curr.X]),
                    new List<Direction>() {curr.Direction}
                );

            foreach(var dir in directions)
            {
                Vector newNode;
                switch (dir)
                {
                    case Direction.EAST:
                        newNode = new(curr.X+1, curr.Y, dir);
                        break;
                    case Direction.WEST:
                        newNode = new(curr.X-1, curr.Y, dir);
                        break;
                    case Direction.NORTH:
                        newNode = new(curr.X, curr.Y-1, dir);
                        break;
                    case Direction.SOUTH:
                        newNode = new(curr.X, curr.Y+1, dir);
                        break;
                    default:
                        newNode = new(curr.X, curr.Y, dir);
                        break;
                }

                if(
                    (newNode.X >= 0 && newNode.X < Array[0].Length) &&
                    (newNode.Y >= 0 && newNode.Y < Array.Length)
                )
                {
                    queue.Enqueue(newNode);
                }
            }
        }
    }

}
