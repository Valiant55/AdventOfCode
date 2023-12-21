using AdventOfCode23.Core.Common;
using System.Reflection.PortableExecutable;

namespace AdventOfCode23.Core.Day20;

public class Solution : ISolution
{
    private readonly IParser<Machines> _parser;
    private string InputFile {  get; set; }

    public Solution(IParser<Machines> parser, string inputFile = @"Day20\input.txt")
    {
        _parser = parser;
        InputFile = inputFile;
    }

    public override long Part01()
    {
        var machines = _parser.Parse(InputFile);
        return machines.PushButton(1000);
    }

    public override long Part02()
    {
        var machines = _parser.Parse(InputFile);
        return machines.PushButtonForFinalMachine();
    }
}
