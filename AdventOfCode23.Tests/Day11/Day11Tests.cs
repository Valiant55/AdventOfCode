using AdventOfCode23.Core.Day11;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day11;

public class Day11Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(374);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples_WithFillFactor10()
    {
        var parser = new Parser();
        var universe = parser.Parse(@"Day11\input.txt");

        var answer = universe.FindDistances(9);

        answer.Should().Be(1030);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples_WithFillFactor100()
    {
        var parser = new Parser();
        var universe = parser.Parse(@"Day11\input.txt");

        var answer = universe.FindDistances(99);

        answer.Should().Be(8410);
    }

    [Theory]
    [InlineData(0,      6)]
    [InlineData(1,      10)]
    [InlineData(99,     402)]
    [InlineData(999,    4002)]
    [InlineData(9999,   40002)]
    [InlineData(99999,  400002)]
    [InlineData(999999, 4000002)]
    public void Part02_ReturnsExpectedValue_FromContrivedData(int fillFactor, int expectedDistance)
    {
        var parser = new Parser();
        var universe = parser.Parse(@"Day11\input2.txt");

        var answer = universe.FindDistances(fillFactor);

        answer.Should().Be(expectedDistance);
    }

    [Theory]
    [InlineData(0,      1)]
    [InlineData(1,      1)]
    [InlineData(99,     1)]
    [InlineData(999,    1)]
    [InlineData(9999,   1)]
    [InlineData(99999,  1)]
    [InlineData(999999, 1)]
    public void Part02_ReturnsExpectedValue_FromContrivedData_WithNoExpansion(int fillFactor, int expectedDistance)
    {
        var parser = new Parser();
        var universe = parser.Parse(@"Day11\input3.txt");

        var answer = universe.FindDistances(fillFactor);

        answer.Should().Be(expectedDistance);
    }
}
