using System.Collections;
using UnityEngine;


public class TheHighPriestess : MajorArcana
{
    public TheHighPriestess(CardSO cardSO) : base(cardSO)
    {
        Name = "The High Priestess";
        FateCost = 2;
        Text = "Learn the number and suit of the next 5 cards.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}