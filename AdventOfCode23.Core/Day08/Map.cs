namespace AdventOfCode23.Core.Day08;

public class Map
{
    public class Node
    {
        public string Left { get; set; }
        public string Right { get; set; }
    }

    public string Directions { get; set; }
    public Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();

    public long StepsRequired()
    {
        int steps = 0;
        var currentNode = Nodes["AAA"];

        foreach(char c in NextDirection())
        {
            var nextId = c switch
            {
                'L' => currentNode.Left,
                'R' => currentNode.Right
            };

            currentNode = Nodes[nextId];
            steps++;

            if (nextId == "ZZZ") break;
        }

        return steps;
    }

    private IEnumerable<char> NextDirection()
    {
        while(true)
        {
            foreach(var c in Directions)
            {
                yield return c;
            }    
        }
    }
}
