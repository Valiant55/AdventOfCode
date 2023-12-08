using AdventOfCode23.Core.Common;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AdventOfCode23.Core.Day04;

public class Parser : IParser<IEnumerable<ScratchCard>>
{
    private readonly Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

    public IEnumerable<ScratchCard> Parse(string inputFile)
    {
        List<ScratchCard> cards = new List<ScratchCard>();

        foreach (var line in File.ReadLines(inputFile))
        {
            var splitLine = line.Split("|");
            var firstHalfMatches = regex.Matches(splitLine[0]);
            var secondHalfMatches = regex.Matches(splitLine[1]);

            var newCard = new ScratchCard() { Id = int.Parse(firstHalfMatches[0].Value) };

            foreach (Match match in firstHalfMatches.Skip(1))
            {
                newCard.WinningNumbers.Add(int.Parse(match.Value));
            }

            foreach (Match match in secondHalfMatches)
            {
                newCard.PlayerNumbers.Add(int.Parse(match.Value));
            }

            cards.Add(newCard);
        }

        return cards;
    }
}
