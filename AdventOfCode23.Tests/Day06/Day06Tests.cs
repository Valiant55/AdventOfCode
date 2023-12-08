using AdventOfCode23.Core.Day06;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day06;

public class Day06Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new RaceListParser();
        var solution = new Solution(parser, null);

        var answer = solution.Part01();

        answer.Should().Be(288);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new RaceParser();
        var solution = new Solution(null, parser);

        var answer = solution.Part02();

        answer.Should().Be(71503);
    }
}
