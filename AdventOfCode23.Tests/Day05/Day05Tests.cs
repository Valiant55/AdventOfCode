using AdventOfCode23.Core.Day05;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day05;

public class Day05Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part01();

        answer.Should().Be(35);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser);

        var answer = solution.Part02();

        answer.Should().Be(46);
    }

    [Theory]
    [InlineData(52, 50, 48, 79,  14, new long[] { 81,  14 })]
    [InlineData(0,  69,  1, 68,   3, new long[] {  0,  1, 68, 1, 70, 1 })]
    [InlineData(68, 64, 13, 74,  14, new long[] { 78,  3, 77, 11})]
    [InlineData(45, 77, 23, 76,  12, new long[] { 76,  1, 45, 11 })]
    [InlineData(10, 20, 20,  5,  50, new long[] {  5, 15, 10, 20, 40, 15 })]
    [InlineData(10, 20, 20,  5,  30, new long[] {  5, 15, 10, 15 })]
    [InlineData(10, 20, 20,  20, 25, new long[] { 10, 20, 40, 5})]
    public void Map_GetMappedRanges_ReturnsCorrectly(
        long destinationStart,
        long sourceStart,
        long length,
        long inputRangeStart,
        long inputRangeLength,
        long[] output
        )
    {
        Map map = new();
        map.DestinationStart.Add(destinationStart);
        map.SourceStart.Add(sourceStart);
        map.Length.Add(length);

        var input = new Core.Day05.Range() {
            Start = inputRangeStart,
            Length = inputRangeLength
        };

        List<Core.Day05.Range> result = map.GetMappedRanges(input);
        List<Core.Day05.Range> expectedOutput = output
            .Chunk(2)
            .Select(c => new Core.Day05.Range { Start = c[0], Length = c[1] })
            .ToList();

        result.Should().BeEquivalentTo(expectedOutput);
    }
}
