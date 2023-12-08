using AdventOfCode23.Core.Day03;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day03;

public class Day03Tests
{

    [Fact]
    public void FindValidParts_ReturnsCorrectValueFromExample()
    {
        var partParser = new PartNoParser();
        var gearParser = new GearParser();
        var engineParser = new EngineParser();

        var solution = new Solution(partParser, gearParser, engineParser);

        var sum = solution.Part01();

        sum.Should().Be(4361);
    }

    [Fact]
    public void FindGearRatioSum_ReturnsCorrectValueFromExample()
    {
        var partParser = new PartNoParser();
        var gearParser = new GearParser();
        var engineParser = new EngineParser();

        var solution = new Solution(partParser, gearParser, engineParser);

        var sum = solution.Part02();

        sum.Should().Be(467835);
    }
}
