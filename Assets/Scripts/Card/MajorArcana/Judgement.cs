using System.Collections;
using UnityEngine;


public class Judgement : MajorArcana
{
    public Judgement(CardSO cardSO) : base(cardSO)
    {
        Name = "Judgement";
        FateCost = 8;
        Text = "Set your Doom to 0. Then move all Death cards in your deck down by the amount of Doom removed.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}