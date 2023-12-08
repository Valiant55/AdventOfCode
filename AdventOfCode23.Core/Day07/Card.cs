namespace AdventOfCode23.Core.Day07;

public enum CardValue
{
    ACE = 14,
    KING = 13,
    QUEEN = 12,
    JACK = 11,
    TEN = 10,
    NINE = 9,
    EIGHT = 8,
    SEVEN = 7,
    SIX = 6,
    FIVE = 5,
    FOUR = 4,
    THREE = 3,
    TWO = 2,
    WILD_JACK = 0
}

public class Card: IComparable<Card>
{
    public CardValue Value { get; private set; }

    private Dictionary<char, CardValue> _cardMap = new()
    {
        { 'A', CardValue.ACE },
        { 'K', CardValue.KING },
        { 'Q', CardValue.QUEEN },
        { 'J', CardValue.JACK },
        { 'T', CardValue.TEN },
        { '9', CardValue.NINE },
        { '8', CardValue.EIGHT },
        { '7', CardValue.SEVEN },
        { '6', CardValue.SIX },
        { '5', CardValue.FIVE },
        { '4', CardValue.FOUR },
        { '3', CardValue.THREE },
        { '2', CardValue.TWO },
        { 'W', CardValue.WILD_JACK }
    };

    private Card(char value)
    {
        Value = _cardMap[value];
    }

    private Card(CardValue value)
    {
        Value = value;
    }

    public static Card Create(char value, bool jacksWild = false)
    {
        if (value == 'J' && jacksWild) return new Card('W');
        return new Card(value);
    }

    public static Card Create(CardValue value)
    {
        return new Card(value);
    }

    public int CompareTo(Card? other)
    {
        if(this.Value < other.Value) return -1;
        if(this.Value > other.Value) return  1;
        return 0;
    }
}
