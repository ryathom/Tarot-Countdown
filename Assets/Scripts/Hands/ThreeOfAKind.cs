using System.Collections.Generic;

public class ThreeOfAKind : Hand
{
    public List<Card> cards;

    public ThreeOfAKind(Card card1, Card card2, Card card3)
    {
        cards = new()
        {
            card1, card2, card3
        };
    }

    public static ThreeOfAKind GetThreeOfAKind(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            foreach (Card match in cards)
            {
                if (match == card) continue;

                if (match.Number == card.Number)
                {
                    foreach (Card match2 in cards)
                    {
                        if (match2 == card || match2 == match) continue;

                        if (match.Number == card.Number)
                        {
                            return new ThreeOfAKind(card, match, match2);
                        }
                    }
                } 
            }
        }

        return null;
    }
}
