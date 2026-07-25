using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheHighPriestess : MajorArcana
{
    public TheHighPriestess(CardSO cardSO) : base(cardSO)
    {
        Name = "The High Priestess";
        FateCost = 4;
        Text = "Look at the top 5 cards of your deck. Choose one and add it to your hand.";
    }

    private Card selectedCard = null;

    public override IEnumerator ExecuteEffect()
    {
        List<Card> top5 = new();
        for (int i = 0; i < 5; i++)
        {
            top5.Add(GameManager.Instance.Deck.Cards[i]);
        }

        UIManager.Instance.OpenBrowser(GameManager.Instance.Deck, canClose: false, subset: top5);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 5; i++)
        {
            GameManager.Instance.Deck.Cards[i].Container.Flip();
            yield return new WaitForSeconds(0.05f);
        }

        // Refresh browser
        UIManager.Instance.OpenBrowser(GameManager.Instance.Deck, canClose: false, subset: top5);
        
        GameManager.Instance.Deck.OnClickCardInDeck += SelectCard;

        while(selectedCard == null)
        {
            yield return null;
        }

        GameManager.Instance.Deck.OnClickCardInDeck -= SelectCard;

        yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(selectedCard, GameManager.Instance.Hand));

        UIManager.Instance.CloseBrowser();
    }

    public void SelectCard(Card card)
    {
        if (card is Death) return;
        
        selectedCard = card;
    }
}