using AdventOfCode23.Core.Day10;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day10;

public class Day10Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromFirstExample()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(4);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromSecondExample()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day10\input2.txt");

        var answer = solution.Part01();

        answer.Should().Be(8);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromFirstExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day10\input3.txt");

        var answer = solution.Part02();

        answer.Should().Be(4);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromSecondExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day10\input4.txt");

        var answer = solution.Part02();

        answer.Should().Be(8);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromThirdExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day10\input5.txt");

        var answer = solution.Part02();

        answer.Should().Be(10);
    }
}
