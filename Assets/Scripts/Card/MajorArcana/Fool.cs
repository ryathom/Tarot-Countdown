using System.Collections;
using UnityEngine;

public class Fool : MajorArcana
{
    public Fool(CardSO cardSO) : base(cardSO)
    {
        Name = "The Fool";
        FateCost = 10;
        Text = "Shuffle all cards back into your deck and draw a new hand. <i>(You can't draw the Death card this turn.)</i>";
    }

    public override IEnumerator ExecuteEffect()
    {
        SoundFXManager.Instance.PlaytarotSoundClip(GameManager.Instance.transform);
        while (GameManager.Instance.PlayArea.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(GameManager.Instance.PlayArea.Cards[^1], GameManager.Instance.Deck, 0.01f));
        }

        while (GameManager.Instance.DiscardPile.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(GameManager.Instance.DiscardPile.Cards[^1], GameManager.Instance.Deck, 0.01f));
        }

        while (GameManager.Instance.Hand.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(GameManager.Instance.Hand.Cards[^1], GameManager.Instance.Deck, 0.01f));
        }

        GameManager.Instance.Deck.Shuffle();

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.Instance.Deck.Cards[i] is Death death)
            {   
                GameManager.Instance.Deck.RemoveCard(death);
                GameManager.Instance.Deck.InsertCard(death, Random.Range(6, GameManager.Instance.Deck.Cards.Count));
            }
        }

        yield return new WaitForSeconds(0.25f);

        GameManager.Instance.Deck.UpdateVisuals();
    }
}

