using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day10;

public class Parser : IParser<PipeMap>
{
    public PipeMap Parse(string inputFile)
    {
        List<List<Pipe>> pipes = new List<List<Pipe>>();
        Position starting = new Position(0,0);

        foreach(string line in File.ReadLines(inputFile))
        {
            List<Pipe> nextLine = new List<Pipe>();
            foreach(char c in line)
            { 
                if(c == 'S')
                {
                    starting = new Position(nextLine.Count, pipes.Count);
                }
                nextLine.Add(new Pipe(c));
            }

            pipes.Add(nextLine);
        }

        return new PipeMap() { Pipes = pipes.To2dArray<Pipe>(), StartingPosition = starting};
    }

}
