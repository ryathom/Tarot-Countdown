using System.Collections;
using UnityEngine;


public class TheHighPriestess : MajorArcana
{
    public TheHighPriestess(CardSO cardSO) : base(cardSO)
    {
        Name = "The High Priestess";
        FateCost = 2;
        Text = "Learn the number and suit of the next 5 cards.";
    }

    public override IEnumerator ExecuteEffect()
    {
        UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 5; i++)
        {
            GameManager.Instance.Deck.Cards[i].SetFaceUp(true);
        }

        // Refresh browser
        UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
        
        yield return new WaitForSeconds(1f);
        UIManager.Instance.CloseBrowser();
    }
}