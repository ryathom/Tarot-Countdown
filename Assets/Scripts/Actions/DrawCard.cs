using System.Collections;
using UnityEngine;

public class DrawCard : IAction
{
    public DrawCard()
    {
    }

    private readonly float delay = 0.25f;

    public IEnumerator Execute()
    {
        Deck deck = GameManager.Instance.Deck;
        Hand hand = GameManager.Instance.Hand;
        Card card = deck.Cards[0];
        
        deck.RemoveCard(card);
        hand.AddCard(card);

        yield return new WaitForSeconds(delay);
    }
}

public class PlayCard : IAction
{
    public Card Card {get; private set;}

    public PlayCard(Card card)
    {
        Card = card;
    }

    private readonly float delay = 0.25f;

    public IEnumerator Execute()
    {
        Hand hand = GameManager.Instance.Hand;
        PlayArea playArea = GameManager.Instance.PlayArea;
        
        hand.RemoveCard(Card);
        playArea.AddCard(Card);

        yield return new WaitForSeconds(delay);
    }
}