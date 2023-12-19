using AdventOfCode23.Core.Day18;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day18;

public class Day18Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(62);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromMiddleOrigin()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day18\input2.txt");

        var answer = solution.Part01();

        answer.Should().Be(76);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part02();

        answer.Should().Be(952408144115);
    }
}
