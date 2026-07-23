using System.Collections;
using UnityEngine;


public class TheTower : MajorArcana
{
    public TheTower(CardSO cardSO) : base(cardSO)
    {
        Name = "The Tower";
        FateCost = 7;
        Text = "Reverse your deck.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}