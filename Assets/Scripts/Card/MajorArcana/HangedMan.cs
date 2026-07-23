using System.Collections;
using UnityEngine;

public class HangedMan : MajorArcana
{
    public HangedMan(CardSO cardSO) : base(cardSO)
    {
        Name = "The Hanged Man";
        FateCost = 7;
        Text = "If there is a Death card in your next 3 cards, send it to the bottom of your deck.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}