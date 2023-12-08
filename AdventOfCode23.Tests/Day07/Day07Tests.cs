using AdventOfCode23.Core.Day07;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day07;

public class Day07Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(6440);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part02();

        answer.Should().Be(5905);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromRedditEdgeCases()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day07\reddit_edge.txt");

        var answer = solution.Part01();

        answer.Should().Be(6592);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromRedditEdgeCases()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day07\reddit_edge.txt");

        var answer = solution.Part02();

        answer.Should().Be(6839);
    }
}
