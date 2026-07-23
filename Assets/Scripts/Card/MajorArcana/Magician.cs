using System.Collections;
using UnityEngine;


public class Magician : MajorArcana
{
    public Magician(CardSO cardSO) : base(cardSO)
    {
        Name = "The Magician";
        FateCost = 1;
        Text = "During your next draw, if you would draw a Death card, negate its effect and place it on the bottom of your deck instead.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}