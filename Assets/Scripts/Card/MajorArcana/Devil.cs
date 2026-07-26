using System.Collections;
using UnityEngine;

public class Devil : MajorArcana
{
    public Devil(CardSO cardSO) : base(cardSO)
    {
        Name = "The Devil";
        FateCost = 5;
        Text = "Gain 1 Doom. Send the Death card to the bottom of the deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        SoundFXManager.Instance.PlaytarotSoundClip(GameManager.Instance.transform);
        GameManager.Instance.GainDoom(1);
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < GameManager.Instance.Deck.Cards.Count; i++)
        {
            if (GameManager.Instance.Deck.Cards[i] is Death death)
            {
                UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
                yield return new WaitForSeconds(1f);
                
                GameManager.Instance.Deck.RemoveCard(death);
                GameManager.Instance.Deck.InsertCard(death, GameManager.Instance.Deck.Cards.Count);
                
                // Refresh browser
                UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
                
                yield return new WaitForSeconds(2f);
                UIManager.Instance.CloseBrowser();
                
                yield break;
            }
        }
    }
}

