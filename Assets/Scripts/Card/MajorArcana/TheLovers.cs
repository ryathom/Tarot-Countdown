using System.Collections;
using UnityEngine;


public class TheLovers : MajorArcana
{
    public TheLovers(CardSO cardSO) : base(cardSO)
    {
        Name = "The Lovers";
        FateCost = 5;
        Text = "The next card you play is considered LOWER than the previous card.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}