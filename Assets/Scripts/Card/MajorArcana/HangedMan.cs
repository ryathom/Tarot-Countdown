using System.Collections;
using UnityEngine;

public class HangedMan : MajorArcana
{
    public HangedMan(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hanged Man";
        FateCost = 7;
        Text = "If there is a Death card in your next 3 cards, send it to the bottom of your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.Deck.Cards[i] is Death death)
            {
                UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
                yield return new WaitForSeconds(1f);
                
                GameManager.Instance.Deck.RemoveCard(death);
                GameManager.Instance.Deck.InsertCard(death, GameManager.Instance.Deck.Cards.Count - 1);
                
                // Refresh browser
                UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
                
                yield return new WaitForSeconds(2f);
                UIManager.Instance.CloseBrowser();
                
                yield break;
            }
        }
    }
}