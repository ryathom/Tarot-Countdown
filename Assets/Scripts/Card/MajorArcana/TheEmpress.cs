using System.Collections;
using UnityEngine;


public class TheEmpress : MajorArcana
{
    public TheEmpress(CardSO cardSO) : base(cardSO)
    {
        Name = "The Empress";
        FateCost = 3;
        Text = "Create a new 3 of each suit and place them in random positions in the deck.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}