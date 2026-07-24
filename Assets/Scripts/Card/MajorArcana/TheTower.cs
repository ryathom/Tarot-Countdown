using System.Collections;
using UnityEngine;


public class TheTower : MajorArcana
{
    public TheTower(CardSO cardSO) : base(cardSO)
    {
        Name = "The Tower";
        FateCost = 7;
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