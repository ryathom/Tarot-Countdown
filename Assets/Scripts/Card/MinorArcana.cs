public class MinorArcana : Card
{
    public MinorArcana(CardSO cardSO, int number, Suit suit) : base(cardSO)
    {
        Number = number;
        Suit = suit;
        Name = NumberString(Number) + " of " + Suit;
    }

    public string NumberString(int number)
    {
        return number switch
        {
            14 => "King",
            13 => "Queen",
            12 => "Knight",
            11 => "Page",
            1 => "Ace",
            _ => number.ToString(),
        };
    }
}

public enum Suit
{
    Wands,
    Swords,
    Cups,
    Coins
}

