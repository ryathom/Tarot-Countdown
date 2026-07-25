using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheEmperor : MajorArcana
{
    public TheEmperor(CardSO cardSO) : base(cardSO)
    {
        Name = "The Emperor";
        FateCost = 6;
        Text = "Put the next ten cards of your deck into descending order. <i>(Death is 0)</i>";
    }

    public override IEnumerator ExecuteEffect()
    {
        Deck deck = GameManager.Instance.Deck;
        UIManager.Instance.OpenBrowser(deck, canClose: false);
        yield return new WaitForSeconds(0.5f);

        OrderCards(10);

        UIManager.Instance.OpenBrowser(deck, canClose: false);
        yield return new WaitForSeconds(1f);
        UIManager.Instance.CloseBrowser();
    }

    public void OrderCards(int n)
    {
        Deck deck = GameManager.Instance.Deck;
        List<Card> cardsToOrder = new();

        for (int i = 0; i < n; i++)
        {
            cardsToOrder.Add(deck.Cards[i]);
            deck.RemoveCard(deck.Cards[i]);
        }

        cardsToOrder.Sort(delegate(Card x, Card y)
        {
            if (x.Number == y.Number) return 0;
            else if (x.Number > y.Number) return 1;
            else return -1;
        });

        for (int i = 0; i < n; i++)
        {
            deck.InsertCard(cardsToOrder[0], 0);
            cardsToOrder.Remove(cardsToOrder[0]);
        }
    }
    
}