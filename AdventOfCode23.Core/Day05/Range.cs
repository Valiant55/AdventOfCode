namespace AdventOfCode23.Core.Day05;

public class Range
{
    public long Start { get; set; }
    public long Length { get; set; }

    public long End => Start + Length - 1;
}
