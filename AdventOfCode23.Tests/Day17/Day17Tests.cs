﻿using AdventOfCode23.Core.Day17;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day17;

public class Day17Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(102);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part02();

        answer.Should().Be(94);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromSecondExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day17\input2.txt");

        var answer = solution.Part02();

        answer.Should().Be(71);
    }
}
