using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Strength : MajorArcana
{
    public Strength(CardSO cardSO) : base(cardSO)
    {
        Name = "Strength";
        FateCost = 5;
        Text = "Increase the value of two cards in your hand by 1.";
    }

    private List<Card> selectedCards = new();

    public override IEnumerator ExecuteEffect()
    {
        HandArea hand = GameManager.Instance.Hand;
        UIManager.Instance.OpenBrowser(hand, canClose: false);

        hand.OnClickCardInHand += SelectCard;
        
        while(selectedCards.Count < 2)
        {
            yield return null;
        }

        selectedCards.Clear();
        
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.CloseBrowser();
    }

    public void SelectCard(Card card)
    {
        selectedCards.Add(card);

        if (card.Number == 14)
        {
            card.Number = 1;
        } else
        {
            card.Number += 1;
        }

        card.Container.ShowVisual(true);
    }
}