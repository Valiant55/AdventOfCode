namespace AdventOfCode23.Core.Common;

public interface IParser<T> where T : class
{
    T Parse(string inputFile);
}
