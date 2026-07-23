using System.Collections;

public class Sun : MajorArcana
{
    public Sun(CardSO cardSO) : base(cardSO)
    {
        Name = "The Sun";
        FateCost = 4;
        Text = "Shuffle all Cups from your discard pile back into your deck.";
    }

    public override IEnumerator ExecuteEffect()
    {
        yield return ShuffleBackSuit(Suit.Cups);
    }
}

