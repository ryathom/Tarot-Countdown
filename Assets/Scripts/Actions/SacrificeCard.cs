using System.Collections;
using UnityEngine;

public class SacrificeCard : IAction
{
    public Card Card {get; private set;}

    public SacrificeCard(Card card)
    {
        Card = card;
    }

    public IEnumerator Execute()
    {
        if (Card is MinorArcana && GameManager.Instance.CanSacrifice)
        {
            GameManager.Instance.GainFate(Card.Number / 2);
            GameManager.Instance.Hand.RemoveCard(Card);
            GameManager.Instance.DestroyCard(Card);
            GameManager.Instance.CanSacrifice = false;

            GameManager.Actions.AddAction(new DrawCard());

            yield return new WaitForSeconds(1f);
        }
    }
}
