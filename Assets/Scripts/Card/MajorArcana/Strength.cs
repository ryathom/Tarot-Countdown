using System.Collections;
using UnityEngine;


public class Strength : MajorArcana
{
    public Strength(CardSO cardSO) : base(cardSO)
    {
        Name = "Strength";
        FateCost = 5;
        Text = "Increase the value of two cards in your hand by 1.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}