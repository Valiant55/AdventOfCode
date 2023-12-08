using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day03;

public class PartNo
{
    public int Id { get; set; }
    public int LineNo { get; set; }
    public int Position { get; set; }
    public int Length { get; set; }
    public List<Position> Neighbors { get; set; } = new List<Position>();

    public PartNo(int id, int lineNo, int position, int length)
    {
        Id = id;
        LineNo = lineNo;
        Position = position;
        Length = length;

        int[] y = { lineNo - 1, lineNo, lineNo + 1 };
        int[] x = Enumerable.Range(position - 1, length + 2).ToArray();
        Neighbors =
            x.SelectMany(x => y, (a, b) => new Position(a, b))
            .Where(p => p.X >= 0 && p.Y >= 0)
            .ToList();
    }
}