using AdventOfCode23.Core.Day01;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day01;

public class Day01Tests
{
    [Theory]
    [InlineData("two1nine", 11)]
    [InlineData("eightwo78three", 78)]
    [InlineData("abcone2threexyz", 22)]
    [InlineData("xtwone3four", 33)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 24)]
    [InlineData("7pqrstsixteen", 77)]
    [InlineData("eigh8two", 88)]

    public void Part01_ReturnsExpectedValue_FromExamples(string line, int digit)
    {
        var result = Calibration.FindCalibrationNumber(line);

        result.Should().Be(digit);
    }

    [Theory]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("eightwo", 82)]

    public void Part02_ReturnsExpectedValue_FromExamples(string line, int digit)
    {
        var result = Calibration.FindCalibrationNumberWithText(line);

        result.Should().Be(digit);
    }
}