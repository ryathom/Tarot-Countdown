using System.Collections;
using UnityEngine;

public class DrawCard : IAction
{
    public DrawCard()
    {
    }

    public IEnumerator Execute()
    {
        Deck deck = GameManager.Instance.Deck;

        if (deck.Cards.Count <= 0) yield break;

        HandArea hand = GameManager.Instance.Hand;
        Card card = deck.Cards[0];
        
        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(card, hand));

        if (card is Death)
        {
            Debug.Log("Game over");
        }
    }
}
