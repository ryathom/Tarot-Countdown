using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chariot : MajorArcana
{
    public Chariot(CardSO cardSO) : base(cardSO)
    {
        Name = "The Chariot";
        FateCost = 3;
        Text = "Choose two cards from your discard pile. Randomly place them in the top 10 cards of your deck.";
    }

    private List<Card> selectedCards = new();

    public override IEnumerator ExecuteEffect()
    {
        DiscardPile discardPile = GameManager.Instance.DiscardPile;
        UIManager.Instance.OpenBrowser(discardPile, canClose: false);

        discardPile.OnClickCardInDiscardPile += SelectCard;
        
        while(selectedCards.Count < 2)
        {
            yield return null;
        }

        ReturnCards();
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.CloseBrowser();
    }

    public void SelectCard(Card card)
    {
        selectedCards.Add(card);
    }

    public void ReturnCards()
    {
        while(selectedCards.Count > 0)
        {
            Card card = selectedCards[0];
            selectedCards.Remove(card);
            card.Zone.RemoveCard(card);
            GameManager.Instance.Deck.InsertCard(card, Random.Range(0, 10));
        }
    }
}