using AdventOfCode23.Core.Day01;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day01;

public class CalibrationTests
{
    [Theory]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("eightwo", 82)]

    public void FindCalibrationNumber_FromExamples_ReturnsExpectedValue(string line, int digit)
    {
        var result = Calibration.FindCalibrationNumber(line);

        result.Should().Be(digit);
    }
}