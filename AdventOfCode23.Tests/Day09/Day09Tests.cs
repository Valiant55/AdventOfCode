using AdventOfCode23.Core.Day09;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day09;

public class Day09Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(114);
    }

    [Fact]
    public void Part01_ReturnsExpectedValue_FromCherryPickedExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, @"Day09/input2.txt");

        var answer = solution.Part01();

        answer.Should().Be(-1363);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part02();

        answer.Should().Be(0);
    }

    [Theory]
    [InlineData(new long[] {  0, 3,  6,   9, 12, 15 },   new long[] {0, 3, 18})]
    [InlineData(new long[] {  0, -3, -6, -9, -12, -15 }, new long[] { 0, -3, -18 })]
    [InlineData(new long[] {  1, 3,  6,  10, 15, 21 },   new long[] {0, 1,  7, 28})]
    [InlineData(new long[] { 10, 13, 16, 21, 30, 45 },   new long[] {0, 2,  8, 23, 68})]
    public void Reading_ReturnsCorrectPredictions(long[] readingInput, long[] predictionOutput)
    {
        var reading = new Reading() { Readings = readingInput.ToList()};
        var preditions = reading.PredicatedReadings();

        preditions.Should().BeEquivalentTo(predictionOutput);
    }
    [Theory]
    [InlineData(new long[] { 0, 3, 6, 9, 12, 15 },       18 )]
    [InlineData(new long[] { 0, -3, -6, -9, -12, -15 }, -18 )]
    [InlineData(new long[] { 1, 3, 6, 10, 15, 21 },      28 )]
    [InlineData(new long[] { 10, 13, 16, 21, 30, 45 },   68 )]
    public void Reading_ReturnsCorrectPrediction(long[] readingInput, long predictionOutput)
    {
        var reading = new Reading() { Readings = readingInput.ToList() };
        var preditions = reading.PredicatedReadings();

        preditions.Last().Should().Be(predictionOutput);
    }

}
