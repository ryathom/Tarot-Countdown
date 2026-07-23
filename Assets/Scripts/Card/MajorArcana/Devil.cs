using System.Collections;
using UnityEngine;

public class Devil : MajorArcana
{
    public Devil(CardSO cardSO) : base(cardSO)
    {
        Name = "The Devil";
        FateCost = 1;
        Text = "If you have more than 3 Death cards in your deck, reduce that number to 1.";
    }

    public override IEnumerator ExecuteEffect()
    {
        while (GameManager.Instance.Deck.DeathCount() > 1) {
            foreach(Card card in GameManager.Instance.Deck.Cards)
            {
                if (card is Death)
                {
                    GameManager.Instance.DestroyCard(card);
                    break;
                }
            }
        }

        return null;
    }
}

