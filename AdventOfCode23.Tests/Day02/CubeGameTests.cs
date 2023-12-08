using AdventOfCode23.Core.Day02;
using FluentAssertions;

namespace AdventOfCode23.Tests.Day02;

public class CubeGameTests
{
    [Theory]
    [InlineData("3 blue, 4 red", 4, 3, 0)]
    [InlineData("8 green, 6 blue, 20 red", 20, 6, 8)]
    [InlineData("3 green, 15 blue, 14 red", 14, 15, 3)]
    public void ParsingGameResultString_CreatesCorrectGameResultObject(string gameResult, int redCubes, int blueCubes, int greenCubes)
    {
        var cubeGame = new CubeGame();

        var result = cubeGame.ParseGameResult(gameResult);

        result.RedCubes.Should().Be(redCubes);
        result.GreenCubes.Should().Be(greenCubes);
        result.BlueCubes.Should().Be(blueCubes);
    }

    [Fact]
    public void FindPossibleGameSum_ReturnsCorrectValueFromExample()
    {
        var cubeGame = new CubeGame();

        int sum = cubeGame.FindPossibleGameSum(@"Day02\input.txt");

        sum.Should().Be(8);
    }

    [Fact]
    public void FindPossibleGamePower_ReturnsCorrectValueFromExample()
    {
        var cubeGame = new CubeGame();

        int sum = cubeGame.FindPossibleGamePower(@"Day02\input.txt");

        sum.Should().Be(2286);
    }
}
