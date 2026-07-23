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
        FateCost = 1;
    }

    public override IEnumerator ExecuteEffect()
    {
        GameManager.Instance.Deck.Shuffle();
        GameManager.Instance.Deck.UpdateVisuals();

        return null;
    }
}

