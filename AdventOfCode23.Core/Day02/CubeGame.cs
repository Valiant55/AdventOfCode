using AdventOfCode23.Core.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day02;

public class CubeGame
{
    public class Game
    {
        public int Id { get; set; }

        public List<GameResults> GameResults { get; set; } = new List<GameResults>();

        public int MaxRed => GameResults.Select(g => g.RedCubes).Max();
        public int MaxGreen => GameResults.Select(g => g.GreenCubes).Max();
        public int MaxBlue => GameResults.Select(g => g.BlueCubes).Max();
    }

    public class GameResults
    {
        public int RedCubes { get; set; } = 0;
        public int GreenCubes { get; set; } = 0;
        public int BlueCubes { get; set; } = 0;
    }

    public int MaxRedCubes { get; set; } = 12;
    public int MaxGreenCubes { get; set; } = 13;
    public int MaxBlueCubes { get; set; } = 14;

    private readonly Regex gameRegex = new Regex(@"\d+|red|blue|green", RegexOptions.Compiled);

    public int FindPossibleGameSum(string document = @"Day02\CubeGameResults.txt")
    {
        var games = ImportGames(document);
        var sum = games
            .Where(g =>
            {
                return g.MaxRed <= MaxRedCubes &&
                       g.MaxGreen <= MaxGreenCubes &&
                       g.MaxBlue <= MaxBlueCubes;
            })
            .Select(g => g.Id)
            .Sum();

        return sum;
    }

    public int FindPossibleGamePower(string document = @"Day02\CubeGameResults.txt")
    {
        var games = ImportGames(document);
        var sum = games
            .Select(g => g.MaxRed * g.MaxGreen * g.MaxBlue)
            .Sum();

        return sum;
    }

    public List<Game> ImportGames(string document)
    {
        List<Game> games = new List<Game>();

        foreach(var line in FileReader.Read(document))
        {
            var matches = gameRegex.Matches(line);
            var gameId = int.Parse(matches[0].Value);
            var currentGame = new Game() { Id = gameId };

            var gameResults = line.Split(":")[1].Split(";");
            foreach(var game in gameResults)
            {
                var results = ParseGameResult(game);
                currentGame.GameResults.Add(results);
            }

            games.Add(currentGame);
        }

        return games;
    }

    public GameResults ParseGameResult(string game)
    {
        var gameMatches = gameRegex.Matches(game);
        var results = new GameResults();
        int lastDigit = 0;

        foreach (var match in gameMatches.Select(m => m.Value))
        {
            var isDigit = int.TryParse(match, out var digit);

            if (isDigit) lastDigit = digit;
            else
            {
                switch (match)
                {
                    case "red":
                        results.RedCubes = lastDigit;
                        break;
                    case "green":
                        results.GreenCubes = lastDigit;
                        break;
                    case "blue":
                        results.BlueCubes = lastDigit;
                        break;
                }
            }
        }

        return results;
    }
}
