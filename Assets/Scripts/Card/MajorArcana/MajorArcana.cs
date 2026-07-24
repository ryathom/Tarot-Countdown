using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MajorArcana : Card
{
    public MajorArcana(CardSO cardSO) : base(cardSO)
    {
    }

    public int FateCost;
    public string Text;

    public abstract IEnumerator ExecuteEffect();

    public IEnumerator ShuffleBackSuit(Suit suit)
    {
        List<Card> cardsToShuffle = new();

        foreach (Card card in GameManager.Instance.DiscardPile.Cards)
        {
            if (card.Suit == suit)
            {
                cardsToShuffle.Add(card);
            }
        }

        while(cardsToShuffle.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(cardsToShuffle[0], GameManager.Instance.Deck));
            cardsToShuffle.Remove(cardsToShuffle[0]);
        }

        GameManager.Instance.Deck.Shuffle();
    }
}