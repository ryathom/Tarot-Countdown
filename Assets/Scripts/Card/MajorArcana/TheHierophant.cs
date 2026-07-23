using System.Collections;
using UnityEngine;


public class TheHierophant : MajorArcana
{
    public TheHierophant(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hierophant";
        FateCost = 5;
        Text = "The next card you play is considered the SAME SUIT as the previous card played.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}