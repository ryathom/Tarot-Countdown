using System.Collections;
using UnityEngine;


public class Chariot : MajorArcana
{
    public Chariot(CardSO cardSO) : base(cardSO)
    {
        Name = "The Chariot";
        FateCost = 3;
        Text = "Choose two cards from your discard pile. Randomly place them in the top 10 cards of your deck.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}