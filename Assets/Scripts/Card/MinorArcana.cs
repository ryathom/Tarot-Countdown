public class MinorArcana : Card
{
    public MinorArcana(CardSO cardSO, int number, Suit suit) : base(cardSO)
    {
        Number = number;
        Suit = suit;
        Name = Number + " of " + Suit;
    }    
}

public enum Suit
{
    Wands,
    Swords,
    Cups,
    Coins
}

