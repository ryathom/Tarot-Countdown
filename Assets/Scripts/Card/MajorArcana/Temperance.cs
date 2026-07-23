using System.Collections;
using UnityEngine;


public class Temperance : MajorArcana
{
    public Temperance(CardSO cardSO) : base(cardSO)
    {
        Name = "Temperance";
        FateCost = 5;
        Text = "Balance the number of cards in your Discard Pile and Deck.";
    }

    public override IEnumerator ExecuteEffect()
    { return null; }
}