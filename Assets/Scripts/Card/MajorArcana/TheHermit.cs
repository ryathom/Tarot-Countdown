using System.Collections;
using UnityEngine;


public class TheHermit : MajorArcana
{
    public TheHermit(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hermit";
        FateCost = 3;
        Text = "The next three cards you play will mill exactly one card.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}
