using System.Collections;
using UnityEngine;


public class TheEmperor : MajorArcana
{
    public TheEmperor(CardSO cardSO) : base(cardSO)
    {
        Name = "The Emperor";
        FateCost = 10;
        Text = "Reduce your turns by the number of Death cards you currently have in your deck.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}