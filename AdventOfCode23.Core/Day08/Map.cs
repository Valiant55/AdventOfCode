using AdventOfCode23.Core.Common;
using System.Collections.Concurrent;

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

    public long GhostStepsRequired()
    {
        HashSet<string> nextNodes = Nodes.Keys
            .Where(k => k.EndsWith('A'))
            .ToHashSet();

        List<(string, string, int)> stepsRequired = StepsRequiredToNextEndNode(nextNodes);
        List<(string, string, int)> nextStepsRequired = StepsRequiredToNextEndNode(stepsRequired.Select(t => t.Item2));

        return long.Min(
            PrimeFactorProduct(stepsRequired.Select(t => t.Item3).ToList()),
            PrimeFactorProduct(nextStepsRequired.Select(t => t.Item3).ToList())
        );
    }

    private long PrimeFactorProduct(List<int> steps)
    {
        var factors = steps
            .Select(i => Primes.PrimeFactors(i))
            .Select(f => f.GroupBy(i => i).Select(g => new { Factor = g.Key, Value = g.Count() * g.Key }))
            .SelectMany(a => a)
            .GroupBy(a => a.Factor)
            .Select(g => (long)g.Select(g => g.Value).Max());

        return factors.Aggregate((a, x) => (a * x));
    }

    private List<(string, string, int)> StepsRequiredToNextEndNode(IEnumerable<string> startingNodes)
    {
        List<(string, string, int)> result = new();

        foreach(var node in startingNodes)
        {
            int steps = 0;
            var currentNode = Nodes[node];
            string nextId = string.Empty;

            foreach (char c in NextDirection())
            {
                nextId = c switch
                {
                    'L' => currentNode.Left,
                    'R' => currentNode.Right
                };

                currentNode = Nodes[nextId];
                steps++;

                if (nextId.EndsWith('Z')) break;
            }

            result.Add((node, nextId, steps));
        }

        return result;
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
