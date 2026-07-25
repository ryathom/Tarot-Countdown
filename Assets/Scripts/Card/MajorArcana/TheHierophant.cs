using System.Collections;
using UnityEngine;


public class TheHierophant : MajorArcana
{
    public TheHierophant(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hierophant";
        FateCost = 5;
        Text = "Move the Death card down 10 places.";
    }

    public override IEnumerator ExecuteEffect()
    {
        for (int i = 0; i < GameManager.Instance.Deck.Cards.Count; i++)
        {
            if (GameManager.Instance.Deck.Cards[i] is Death death)
            {
                UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
                yield return new WaitForSeconds(1f);
                
                GameManager.Instance.Deck.RemoveCard(death);
                GameManager.Instance.Deck.InsertCard(death, i + 10);
                
                // Refresh browser
                UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
                
                yield return new WaitForSeconds(2f);
                UIManager.Instance.CloseBrowser();
                
                yield break;
            }
        }
    }
}