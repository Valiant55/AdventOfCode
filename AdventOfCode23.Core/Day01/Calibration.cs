using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day01;

/// <summary>
/// https://adventofcode.com/2023/day/1
/// </summary>
public static class Calibration
{
    private static readonly Regex partOne = new Regex(@"\d{1}", RegexOptions.Compiled);
    private static readonly Regex partTwo = new Regex(@"(?=(\d{1}|one|two|three|four|five|six|seven|eight|nine))", RegexOptions.Compiled);
    private static Dictionary<string, int> digitMap = new()
    {
        { "1",     1 },
        { "one",   1 },
        { "2",     2 },
        { "two",   2 },
        { "3",     3 },
        { "three", 3 },
        { "4",     4 },
        { "four",  4 },
        { "5",     5 },
        { "five",  5 },
        { "6",     6 },
        { "six",   6 },
        { "7",     7 },
        { "seven", 7 },
        { "8",     8 },
        { "eight", 8 },
        { "9",     9 },
        { "nine",  9 }

    };

    public static int FindCalibrationNumber(string line)
    {
        var matches = partOne.Matches(line);
        var firstDigit = matches.First().Value;
        var lastDigit = matches.Last().Value;
        var concatDigit = $"{digitMap[firstDigit]}{digitMap[lastDigit]}";
        return int.Parse(concatDigit);
    }

    public static int FindCalibrationNumberWithText(string line)
    {
        var matches = partTwo.Matches(line);
        var firstDigit = matches.First().Groups[1].ToString();
        var lastDigit = matches.Last().Groups[1].ToString();
        var concatDigit = $"{digitMap[firstDigit]}{digitMap[lastDigit]}";
        return int.Parse(concatDigit);
    }
}
