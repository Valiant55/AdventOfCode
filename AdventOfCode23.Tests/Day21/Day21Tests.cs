using AdventOfCode23.Core.Day21;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day21;

public class Day21Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Parser();

        var garden = parser.Parse(@"Day21\input.txt");
        var answer = garden.CountPlots(6);

        answer.Should().Be(16);
    }

    [Theory]
    [InlineData(6,    16)]
    [InlineData(10,   50)]
    [InlineData(50,   1594)]
    [InlineData(100,  6536)]
    [InlineData(500,  167004)]
    [InlineData(1000, 668697)]
    [InlineData(5000, 16733044)]
    public void Part02_ReturnsExpectedValue_FromExamples(int steps, int result)
    {
        var parser = new Parser();

        var garden = parser.Parse(@"Day21\input.txt");
        var answer = garden.CountPlots(steps);

        answer.Should().Be(result);
    }
}
