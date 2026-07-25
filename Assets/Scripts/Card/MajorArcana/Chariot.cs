using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chariot : MajorArcana
{
    public Chariot(CardSO cardSO) : base(cardSO)
    {
        Name = "The Chariot";
        FateCost = 5;
        Text = "Shuffle your discard pile into your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        DiscardPile discardPile = GameManager.Instance.DiscardPile;
        Deck deck = GameManager.Instance.Deck;

        while (discardPile.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(discardPile.Cards[0], deck, 0.05f));
        }

        deck.Shuffle();
        yield return new WaitForSeconds(0.25f);
    }
}