using System.Collections.Generic;

public class Pair : Hand
{
    public List<Card> cards;

    public Pair(Card card1, Card card2)
    {
        cards = new()
        {
            card1, card2
        };
    }

    public static Pair GetPair(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            foreach (Card match in cards)
            {
                if (match == card) continue;
                
                if (match.Number == card.Number)
                {
                    return new Pair(card, match);
                }
            }
        }

        return null;
    }
}
