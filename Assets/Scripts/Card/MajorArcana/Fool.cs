using System.Collections;
using UnityEngine;

public class Fool : MajorArcana
{
    public Fool(CardSO cardSO) : base(cardSO)
    {
        Name = "The Fool";
        FateCost = 10;
        Text = "Shuffle all cards back into your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
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

        yield return new WaitForSeconds(0.25f);

        GameManager.Instance.Deck.UpdateVisuals();
    }
}

