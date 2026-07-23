using System.Collections.Generic;

public static class HandRanker
{
    public static Hand GetHand(List<Card> cards)
    {
        if (ThreeOfAKind.GetThreeOfAKind(cards) != null)
        {
            return ThreeOfAKind.GetThreeOfAKind(cards);
        } 
        else if (Pair.GetPair(cards) != null)
        {
            return Pair.GetPair(cards);
        } else
        {
            return HighCard.GetHighCard(cards);
        }
    }
}