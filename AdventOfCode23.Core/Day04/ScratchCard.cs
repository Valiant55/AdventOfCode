namespace AdventOfCode23.Core.Day04;

public class ScratchCard
{
    public int Id { get; set; }
    public HashSet<int> WinningNumbers { get; set; } = new HashSet<int>();
    public HashSet<int> PlayerNumbers { get; set; } = new HashSet<int>();

    public int PointValue => (int) Math.Pow(2, Matches - 1);
    public int Matches => PlayerNumbers.Intersect(WinningNumbers).Count();
}

