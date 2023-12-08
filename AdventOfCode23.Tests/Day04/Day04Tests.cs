using FluentAssertions;

namespace AdventOfCode23.Tests.Day04;

public class Day04Tests
{
    [Fact]
    public void Part01_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Core.Day04.Parser();
        var soluiton = new Core.Day04.Solution(parser);

        long answer = soluiton.Part01();

        answer.Should().Be(13);
    }

    [Fact]
    public void Part02_ReturnsExpectedValue_FromExamples()
    {
        var parser = new Core.Day04.Parser();
        var soluiton = new Core.Day04.Solution(parser);

        long answer = soluiton.Part02();

        answer.Should().Be(30);
    }
}
