using System.Collections;
using UnityEngine;

public abstract class MajorArcana : Card
{
    public MajorArcana(CardSO cardSO) : base(cardSO)
    {
    }

    public int FateCost;

    public abstract IEnumerator ExecuteEffect();
}

public class Fool : MajorArcana
{
    public Fool(CardSO cardSO) : base(cardSO)
    {
        Name = "The Fool";
        FateCost = 10;
    }

    public override IEnumerator ExecuteEffect()
    {
        while (GameManager.Instance.PlayArea.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(GameManager.Instance.PlayArea.Cards[0], GameManager.Instance.Deck));
        }

        while (GameManager.Instance.DiscardPile.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(GameManager.Instance.DiscardPile.Cards[0], GameManager.Instance.Deck));
        }

        while (GameManager.Instance.Hand.Cards.Count > 0)
        {
            yield return GameManager.Actions.ExecuteImmediate(new ChangeZone(GameManager.Instance.Hand.Cards[0], GameManager.Instance.Deck));
        }

        GameManager.Instance.Deck.Shuffle();
        GameManager.Instance.Deck.UpdateVisuals();
    }
}

