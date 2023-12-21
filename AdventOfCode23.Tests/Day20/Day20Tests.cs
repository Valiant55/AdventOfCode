using AdventOfCode23.Core.Day20;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day20;

public class Day20Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(32000000);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromSecondExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day20\input2.txt");

        var answer = solution.Part01();

        answer.Should().Be(11687500);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromMadeupExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day20\input3.txt");

        var answer = solution.Part01();

        answer.Should().Be(24000000);
    }

}
