using System.Collections;
using UnityEngine;

public class HangedMan : MajorArcana
{
    public HangedMan(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hanged Man";
        FateCost = 6;
        Text = "Reverse your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        Deck deck = GameManager.Instance.Deck;
        UIManager.Instance.OpenBrowser(deck, canClose: false);

        yield return new WaitForSeconds(0.5f);

        deck.Cards.Reverse();

        UIManager.Instance.OpenBrowser(deck, canClose: false);

        yield return new WaitForSeconds(1f);

        UIManager.Instance.CloseBrowser();
    }
}