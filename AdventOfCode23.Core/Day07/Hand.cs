namespace AdventOfCode23.Core.Day07;

public class Hand: IComparable<Hand>
{
    public string HandValue { get; set; }
    public int Bid { get; set; }
    public List<Card> Cards { get; set; }
    public HandType Type { get; set; }

    public enum HandType
    {
        FIVE_OF_A_KIND = 6,
        FOUR_OF_A_KIND = 5,
        FULL_HOUSE = 4,
        THREE_OF_A_KIND = 3,
        TWO_PAIR = 2,
        ONE_PAIR = 1,
        HIGH_CARD = 0
    }

    public Hand(string handValue, int bid)
    {
        HandValue = handValue;
        Bid = bid;
        Cards = new List<Card>();

        foreach(var s in handValue)
        {
            Cards.Add(Card.Create(s));
        }

        Type = GetType(Cards);
    }

    public void UseWildJacks()
    {
        if (!Cards.Any(c => c.Value == CardValue.JACK)) return;

        Cards = new List<Card>();

        foreach (var s in HandValue)
        {
            Cards.Add(Card.Create(s, jacksWild: true));
        }

        var mostCommonCard = Cards
            .Where(c => c.Value != CardValue.WILD_JACK)
            .GroupBy(c => c.Value)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(c => c.Count)
            .Select(a => a.Value)
            .FirstOrDefault(CardValue.WILD_JACK);

        List<Card> replacedCards = Cards.Select(c =>
        {
            if (c.Value == CardValue.WILD_JACK) return Card.Create(mostCommonCard);
            return c;
        })
        .ToList();

        Type = GetType(replacedCards);
    }

    public int CompareTo(Hand? other)
    {
        if(this.Type < other.Type) return -1;
        if(this.Type > other.Type) return 1;

        foreach(var (c1, c2) in this.Cards.Zip(other.Cards))
        {
            int val = c1.CompareTo(c2);
            if (val != 0) return val;
        }

        return 0;
    }

    private HandType GetType(List<Card> cards)
    {
        var agg = cards
            .GroupBy(c => c.Value)
            .Select(g => g.Count())
            .OrderByDescending(c => c)
            .ToList();

        HandType type = agg switch
        {
            [5] => HandType.FIVE_OF_A_KIND,
            [4, 1] => HandType.FOUR_OF_A_KIND,
            [3, 2] => HandType.FULL_HOUSE,
            [3, 1, 1] => HandType.THREE_OF_A_KIND,
            [2, 2, 1] => HandType.TWO_PAIR,
            [2, 1, 1, 1] => HandType.ONE_PAIR,
            _ => HandType.HIGH_CARD
        };

        return type;
    }
}
