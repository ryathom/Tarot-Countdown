using System.Collections;
using UnityEngine;


public class WheelofFortune : MajorArcana
{
    public WheelofFortune(CardSO cardSO) : base(cardSO)
    {
        Name = "Wheel of Fortune";
        FateCost = 5;
        Text = "Discard your hand. Draw 5 new cards.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}