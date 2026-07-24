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
        if (GameManager.Instance.CanSacrifice == false) yield break;

        if (Card is MinorArcana)
        {
            GameManager.Instance.GainFate(Card.Number / 2);
            GameManager.Instance.Hand.RemoveCard(Card);
            GameManager.Instance.DestroyCard(Card);
            GameManager.Instance.CanSacrifice = false;

            GameManager.Actions.AddAction(new DrawCard());
        } else if (Card is MajorArcana)
        {
            GameManager.Instance.GainDoom(-1);
            Card.Zone.RemoveCard(Card);
            GameManager.Instance.DestroyCard(Card);
            GameManager.Instance.CanSacrifice = false;

            GameManager.Actions.AddAction(new DrawTarotCard());
        }

        yield return new WaitForSeconds(0.25f);
    }
}
