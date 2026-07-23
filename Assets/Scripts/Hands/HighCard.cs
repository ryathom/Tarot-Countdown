using System.Collections.Generic;

public class HighCard : Hand
{
    public Card Card;

    public HighCard(Card card)
    {
        Card = card;
    }

    public static HighCard GetHighCard(List<Card> cards)
    {
        if (cards.Count == 0) return null;

        Card highCard = null;

        foreach (Card card in cards)
        {
            highCard ??= card;

            if (card.Number > highCard.Number)
            {
                highCard = card;
            }
        }

        return new HighCard(highCard);
    }
}
