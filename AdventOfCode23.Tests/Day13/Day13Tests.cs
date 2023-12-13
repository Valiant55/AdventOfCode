using AdventOfCode23.Core.Day13;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day13;

public class Day13Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(405);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromMyExample()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day13\input2.txt");

        var answer = solution.Part01();

        answer.Should().Be(202);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromMySecondExample()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day13\input3.txt");

        var answer = solution.Part01();

        answer.Should().Be(304);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromExampleWithMultpleSymmetries()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day13\input4.txt");

        var answer = solution.Part01();

        answer.Should().Be(303);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part02();

        answer.Should().Be(0);
    }
}
