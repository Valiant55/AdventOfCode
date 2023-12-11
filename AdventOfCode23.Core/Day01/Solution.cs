using AdventOfCode23.Core.Common;

namespace AdventOfCode23.Core.Day01;

public class Solution : ISolution
{
    List<string> Document { get; set; }

    public Solution(IParser<List<string>> parser)
    {
        Document = parser.Parse(@"Day01\input.txt");
    }

    public override long Part01()
    {
        return Document.Select(l => Calibration.FindCalibrationNumber(l)).Sum();
    }

    public override long Part02()
    {
        return Document.Select(l => Calibration.FindCalibrationNumberWithText(l)).Sum();
    }
}
