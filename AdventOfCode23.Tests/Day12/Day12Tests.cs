using AdventOfCode23.Core.Day12;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day12;

public class Day12Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();
        var solution = new Solution(parser, parser);

        var answer = solution.Part01();

        answer.Should().Be(21);
    }

    [Theory]
    [InlineData("???.### 1,1,3", 1)]
    [InlineData(".??..??...?##. 1,1,3", 4)]
    [InlineData("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [InlineData("????.#...#... 4,1,1", 1)]
    [InlineData("????.######..#####. 1,6,5", 4)]
    [InlineData("?###???????? 3,2,1", 10)]
    [InlineData(".?#.?.????##?????# 1,1,1,5,1,2", 1)]
    [InlineData("??.?????.?????. 1,2,1,1,1", 47)]
    [InlineData("###.??? 3,1,1", 1)]
    [InlineData("###.??? 3,1", 3)]
    [InlineData("###.??? 3,0", 1)]
    [InlineData("???.### 0,3", 1)]
    public void Part01_ReturnsExpectedValue_FromIndividualExamples(string input, int arrangements)
    {
        string[] parts = input.Split(' ');

        SpringReading reading = new SpringReading(
            parts[0],
            parts[1].Split(',').Select(r => int.Parse(r)).ToList()
        );

        long answer = reading.CountArrangements();

        answer.Should().Be(arrangements);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser5x();
        var solution = new Solution(parser, parser);

        var answer = solution.Part02();

        answer.Should().Be(525152);
    }
}
