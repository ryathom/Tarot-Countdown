using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Judgement : MajorArcana
{
    public Judgement(CardSO cardSO) : base(cardSO)
    {
        Name = "Judgement";
        FateCost = 8;
        Text = "Set your Doom to 0. Then move the Death card down by the amount of Doom removed.";
    }

    public override IEnumerator ExecuteEffect()
    {
        UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
        yield return new WaitForSeconds(0.5f);

        int startingDoom = GameManager.Instance.Doom;
        GameManager.Instance.SetDoom(0);

        List<Card> deathCards = new();

        foreach(Card card in GameManager.Instance.Deck.Cards)
        {
            if (card is Death) deathCards.Add(card);
        }

        foreach(Card death in deathCards)
        {
            int pos = GameManager.Instance.Deck.Cards.IndexOf(death);
            int newPos = Mathf.Clamp(pos + startingDoom, 0, GameManager.Instance.Deck.Cards.Count);

            GameManager.Instance.Deck.RemoveCard(death);
            GameManager.Instance.Deck.InsertCard(death, newPos);
        }

        // Refresh browser
        UIManager.Instance.OpenBrowser(GameManager.Instance.Deck);
        
        yield return new WaitForSeconds(1f);
        UIManager.Instance.CloseBrowser();
    }
}